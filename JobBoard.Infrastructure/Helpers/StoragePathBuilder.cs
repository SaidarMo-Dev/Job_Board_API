using JobBoard.Data.enums;

namespace JobBoard.Infrastructure.Helpers
{
	public static class StoragePathBuilder
	{
		public static string Build<TId>(
			StorageResourceEnum resource,
			TId resourceId,
			string fileName)
			where TId : notnull
		{
			var safeFileName = SanitizeFileName(fileName);
			var uuid = Guid.NewGuid();

			return $"{resource.ToString().ToLower()}/{resourceId}/{uuid}_{safeFileName}";
		}


		private static string SanitizeFileName(string fileName)
		{
			return Path.GetFileName(fileName);
		}
	}

}
