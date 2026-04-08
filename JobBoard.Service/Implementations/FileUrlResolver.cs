using JobBoard.Data.enums;
using JobBoard.Service.Abstractions;

namespace JobBoard.Service.Implementations
{
	public class FileUrlResolver : IFileUrlResolver
	{
		private readonly IFileStorageService _storageService;

		public FileUrlResolver(IFileStorageService storageService)
		{
			_storageService = storageService;
		}

		public string? ResolveCompanyLogo(string? path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return null;

			return _storageService.GetPublicUrl(
				_storageService.GetBucket(FileOwnerType.Companies),
				path);
		}

		public string? ResolveCompanyBanner(string? path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return null;

			return _storageService.GetPublicUrl(
				_storageService.GetBucket(FileOwnerType.Companies),
				path);
		}
	}
}
