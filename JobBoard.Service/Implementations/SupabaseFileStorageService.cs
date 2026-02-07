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
		private readonly IOptions<SupabaseSettings> _settings;

		public SupabaseFileStorageService(IOptions<SupabaseSettings> settigns, Client client)
		{

			_settings = settigns;

			_client = client;
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
			ArgumentNullException.ThrowIfNull(fileStream);
			ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
			ArgumentException.ThrowIfNullOrWhiteSpace(contentType);


			if (fileStream.Length > _settings.Value.MaxFileSizeBytes)
				throw new ArgumentOutOfRangeException(
					nameof(fileStream),
					"File exceeds maximum allowed size.");



			var path = StoragePathBuilder.Build<TId>(resource, resourceId, fileName, filePathType);

			var buffer = new byte[fileStream.Length];
			await fileStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);


			await _client
				.Storage
				.From(_settings.Value.BucketName)
						.Upload(buffer, path, new Supabase.Storage.FileOptions
						{
							ContentType = contentType,
							Upsert = true
						});


			return path;
		}


		public async Task DeleteAsync(string filePath)
		{

			await _client
				.Storage
				.From(_settings.Value.BucketName)
				.Remove(new List<string> { filePath });

		}

		public async Task<string> CreateSignedReadUrlAsync(string filePath)
		{
			var signedUrl = await _client
				.Storage
				.From(_settings.Value.BucketName)
				.CreateSignedUrl(filePath, _settings.Value.SignedUrlExpirySeconds);

			return signedUrl;
		}


	}
}
