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

		public async Task AttachLogosAndBannersAsync<T>(IEnumerable<T> items, Func<T, int> idSelector, Action<T, string?> urlLogoSetter, Action<T, string?> urlBannerSetter, CancellationToken ct = default)
		{
			var itemList = items?.ToList();
			if (itemList == null || !itemList.Any()) return;

			// Get unique IDs using the provided selector
			var companyIds = itemList.Select(idSelector).Distinct().ToList();

			// Batch fetch from DB
			var logos = await _fileService.GetFileResourcesQueryable()
				.Where(f => companyIds.Contains(f.OwnerId)
						 && f.OwnerType == FileOwnerType.Companies
						 && (f.Category == FileCategory.Logo || f.Category == FileCategory.Banner))
				.ToListAsync(ct);

			var lookup = logos.ToLookup(f => f.OwnerId);

			// Assign using the provided setter
			foreach (var item in itemList)
			{
				var logoPath = lookup[idSelector(item)].FirstOrDefault(f => f.Category == FileCategory.Logo)?.Path;
				var bannerPath = lookup[idSelector(item)].FirstOrDefault(f => f.Category == FileCategory.Banner)?.Path;

				urlLogoSetter(item, _urlResolver.ResolveCompanyLogo(logoPath));
				urlBannerSetter(item, _urlResolver.ResolveCompanyLogo(bannerPath));

			}
		}

		public async Task AttachLogosAsync<T>(
			IEnumerable<T> items,
			Func<T, int> idSelector,
			Action<T, string?> urlSetter,
			CancellationToken ct = default)
		{
			var itemList = items?.ToList();
			if (itemList == null || !itemList.Any()) return;

			// Get unique IDs using the provided selector
			var companyIds = itemList.Select(idSelector).Distinct().ToList();

			// Batch fetch from DB
			var logos = await _fileService.GetFileResourcesQueryable()
				.Where(f => companyIds.Contains(f.OwnerId)
						 && f.OwnerType == FileOwnerType.Companies
						 && f.Category == FileCategory.Logo)
				.ToListAsync(ct);

			var lookup = logos.ToLookup(f => f.OwnerId);

			// Assign using the provided setter
			foreach (var item in itemList)
			{
				var path = lookup[idSelector(item)].FirstOrDefault()?.Path;
				urlSetter(item, _urlResolver.ResolveCompanyLogo(path));
			}
		}

		public async Task AttachLogoAndBannerAsync<T>(T item, int Id, Action<T, string?> urlLogoSetter, Action<T, string?> urlBannerSetter, CancellationToken ct = default)
		{
			if (item == null) return;

			var companyFiles = _fileService.GetFileResourcesQueryable()
			.Where(f => f.OwnerId == Id &&
				f.OwnerType == FileOwnerType.Companies &&
				(f.Category == FileCategory.Banner || f.Category == FileCategory.Logo));

			// Extract paths

			var logoPath = (await companyFiles.FirstOrDefaultAsync(f => f.Category == FileCategory.Logo))?.Path;
			var bannerPath = (await companyFiles.FirstOrDefaultAsync(f => f.Category == FileCategory.Banner))?.Path;

			urlLogoSetter(item, _urlResolver.ResolveCompanyLogo(logoPath));
			urlBannerSetter(item, _urlResolver.ResolveCompanyBanner(bannerPath));

		}
	}
}
