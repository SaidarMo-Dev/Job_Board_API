using JobBoard.Data.enums;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{

	public class CompanyFileStitcher : ICompanyFileStitcher
	{
		private readonly IFileResourceService _fileService;
		private readonly IFileUrlResolver _urlResolver;

		public CompanyFileStitcher(IFileResourceService fileService, IFileUrlResolver urlResolver)
		{
			_fileService = fileService;
			_urlResolver = urlResolver;
		}


		public async Task AttachFilesAsync<T>(
		IEnumerable<T> items,
		Func<T, int> idSelector,
		Action<T, string?>? logoSetter = null,
		Action<T, string?>? bannerSetter = null,
		CancellationToken ct = default)
		{
			var itemList = items?.ToList();
			if (itemList == null || !itemList.Any()) return;

			// Determine which categories we actually need to fetch
			var categories = new List<FileCategory?>();
			if (logoSetter != null) categories.Add(FileCategory.Logo);
			if (bannerSetter != null) categories.Add(FileCategory.Banner);

			if (!categories.Any()) return;

			var companyIds = itemList.Select(idSelector).Distinct().ToList();

			// Single DB Query
			var allFiles = await _fileService.GetFileResourcesQueryable()
				.Where(f => companyIds.Contains(f.OwnerId)
						 && f.OwnerType == FileOwnerType.Companies
						 && categories.Contains(f.Category))
				.ToListAsync(ct);

			var lookup = allFiles.ToLookup(f => f.OwnerId);

			foreach (var item in itemList)
			{
				var companyFiles = lookup[idSelector(item)];

				if (logoSetter != null)
				{
					var path = companyFiles.FirstOrDefault(f => f.Category == FileCategory.Logo)?.Path;
					logoSetter(item, _urlResolver.ResolveCompanyLogo(path));
				}

				if (bannerSetter != null)
				{
					var path = companyFiles.FirstOrDefault(f => f.Category == FileCategory.Banner)?.Path;
					bannerSetter(item, _urlResolver.ResolveCompanyBanner(path));
				}
			}
		}

		// Single item implementation:
		public Task AttachFilesAsync<T>(T item, Func<T, int> idSelector, Action<T, string?>? logoSetter = null, Action<T, string?>? bannerSetter = null, CancellationToken ct = default)
			=> AttachFilesAsync(new[] { item }, idSelector, logoSetter, bannerSetter, ct);
	}
}
