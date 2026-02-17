using System.Buffers;
using JobBoard.Data.enums;
using JobBoard.Data.Helpers;
using JobBoard.Infrastructure.Helpers;
using JobBoard.Service.Abstractions;
using Microsoft.Extensions.Options;
using Supabase;

namespace JobBoard.Service.Implementations
{
	public class SupabaseFileStorageService : IFileStorageService
	{
		private readonly Client _client;
		private readonly ISignedUrlCache _signedUrlCache;
		private readonly IFileResourceService _fileResourceService;
		private readonly IOptions<SupabaseSettings> _settings;

		public SupabaseFileStorageService(
			IOptions<SupabaseSettings> settigns,
			Client client,
			ISignedUrlCache signedUrlCache,
			IFileResourceService fileResourceService)
		{

			_settings = settigns;
			_client = client;
			_signedUrlCache = signedUrlCache;
			_fileResourceService = fileResourceService;
		}


		public async Task<string> UploadAsync<TId>(
			Stream fileStream,
			string fileName,
			string contentType,
			FileOwnerType resource,
			TId resourceId,
			FilePathType filePathType,
			CancellationToken cancellationToken)
			where TId : notnull
		{
			// Validate required inputs early to fail fast and avoid undefined behavior later.
			ArgumentNullException.ThrowIfNull(fileStream);
			ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
			ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

			// Perform an upfront size check when the stream length is known.
			// This allows rejecting oversized files early without unnecessary processing.
			if (fileStream.Length > _settings.Value.MaxFileSizeBytes)
				throw new ArgumentOutOfRangeException(
					nameof(fileStream),
					"File exceeds maximum allowed size.");

			// Build the storage path based on the resource type and identifier.
			// This centralizes path construction and enforces a consistent naming strategy.
			var path = StoragePathBuilder.Build<TId>(
				resource,
				resourceId,
				fileName,
				filePathType);

			// Select the target bucket based on the owning resource.
			// Public resources (e.g. companies) are stored in a public bucket,
			// while all others are stored in a private bucket.
			string bucket = (resource == FileOwnerType.Companies)
				? _settings.Value.PublicBucket
				: _settings.Value.PrivateBucket;

			// Reset the stream position when supported to ensure the entire file is read.
			// This is important when the stream has been accessed previously.
			if (fileStream.CanSeek)
				fileStream.Position = 0;

			// Rent a reusable buffer from the shared pool to reduce allocations
			// and improve performance during streaming reads.
			var buffer = ArrayPool<byte>.Shared.Rent(81920);

			try
			{
				// Holds the number of bytes read from the stream on each iteration.
				int read;

				// Accumulates the file content in memory prior to upload.
				// Disposed automatically at the end of the scope.
				using var ms = new MemoryStream();

				// Read the input stream in chunks to avoid loading the entire file at once.
				// This approach is safer and more scalable for large uploads.
				while ((read = await fileStream.ReadAsync(
					buffer.AsMemory(0, buffer.Length),
					cancellationToken)) > 0)
				{
					// Write only the valid portion of the buffer to the memory stream.
					await ms.WriteAsync(buffer.AsMemory(0, read), cancellationToken);

					// Enforce the maximum file size during streaming to prevent
					// excessive memory usage and reject oversized uploads immediately.
					if (ms.Length > _settings.Value.MaxFileSizeBytes)
						throw new ArgumentOutOfRangeException(
							nameof(fileStream),
							"File exceeds maximum allowed size.");
				}

				// Upload the accumulated file bytes to the selected storage bucket.
				// Upsert is enabled to replace any existing file at the same path.
				await _client.Storage
					.From(bucket)
					.Upload(
						ms.ToArray(),
						path,
						new Supabase.Storage.FileOptions
						{
							ContentType = contentType,
							Upsert = true
						});
			}
			finally
			{
				// Always return the rented buffer to the pool, even if an exception occurs,
				// to prevent memory leaks and ensure buffer reuse.
				ArrayPool<byte>.Shared.Return(buffer);
			}

			// Return the final storage path so it can be persisted or referenced by the caller.
			return path;

		}


		public async Task DeleteAsync(FileOwnerType ownerType, string filePath)
		{
			var bucket = GetBucket(ownerType);

			// Delete file from storage
			var result = await _client
				.Storage
				.From(bucket)
				.Remove(new List<string> { filePath });

			// Invalidate signed URL cache after successful deletion
			var cacheKey = $"signed-url:{bucket}:{filePath}";
			await _signedUrlCache.RemoveAsync(cacheKey);


		}

		public async Task<string> CreateSignedReadUrlAsync(string bucket, string filePath)
		{

			var cacheKey = $"signed-url:{bucket}:{filePath}";

			// Try cache
			var cachedUrl = await _signedUrlCache.GetAsync(cacheKey);
			if (!string.IsNullOrEmpty(cachedUrl))
				return cachedUrl;

			// Generate new signed URL

			var signedUrl = await _client
				.Storage
				.From(bucket)
				.CreateSignedUrl(filePath, _settings.Value.SignedUrlExpirySeconds);

			if (signedUrl == null)
				throw new Exception("Failed to generate signed URL.");

			// Cache slightly less than expiration
			var cacheDuration = TimeSpan.FromSeconds(_settings.Value.SignedUrlExpirySeconds - 60);

			await _signedUrlCache.SetAsync(
				cacheKey,
				signedUrl,
				cacheDuration);

			return signedUrl;
		}

		public string GetPublicUrl(string bucket, string filePath)
		{
			var result = _client
				.Storage
				.From(bucket)
				.GetPublicUrl(filePath);

			if (result == null)
				throw new Exception("Failed to generate public URL.");

			return result;
		}

		public string GetBucket(FileOwnerType ownerType)
		{
			return (ownerType == FileOwnerType.Companies) ? _settings.Value.PublicBucket : _settings.Value.PrivateBucket;
		}

		public async Task<Dictionary<int, string>> CreateSignedReadUrlsAsync(string bucket, IEnumerable<int> fileIds)
		{
			var result = new Dictionary<int, string>();
			var ids = fileIds.Distinct().ToList();


			if (!ids.Any())
				return result;

			// Load file paths from repository
			var files = await _fileResourceService
				.GetPathByIdsAsync(ids); // Returns list of { Id, Path }

			if (!files.Any())
				return result;


			var missingFiles = new List<(int Id, string Path)>();

			// Check cache
			foreach (var file in files)
			{
				var cacheKey = $"signed-url:{bucket}:{file.Path}";
				var cachedUrl = await _signedUrlCache.GetAsync(cacheKey);

				if (!string.IsNullOrEmpty(cachedUrl))
				{
					result[file.Id] = cachedUrl;
				}
				else
				{
					missingFiles.Add((file.Id, file.Path));
				}
			}

			// Generate signed URLs for missing files
			if (missingFiles.Any())
			{
				var semaphore = new SemaphoreSlim(10);

				var tasks = missingFiles.Select(async f =>
				{
					await semaphore.WaitAsync();
					try
					{
						var signedUrl = await _client
							.Storage
							.From(bucket)
							.CreateSignedUrl(f.Path, _settings.Value.SignedUrlExpirySeconds);

						if (string.IsNullOrEmpty(signedUrl))
							throw new Exception($"Failed to generate signed URL for file {f.Id}");

						// Cache the signed URL
						var cacheKey = $"signed-url:{bucket}:{f.Path}";
						var cacheDuration = TimeSpan.FromSeconds(_settings.Value.SignedUrlExpirySeconds - 60);
						await _signedUrlCache.SetAsync(cacheKey, signedUrl, cacheDuration);

						return (f.Id, signedUrl);
					}
					finally
					{
						semaphore.Release();
					}
				});

				var generatedUrls = await Task.WhenAll(tasks);

				foreach (var (id, url) in generatedUrls)
				{
					result[id] = url;
				}

			}

			return result;

		}
	}
}
