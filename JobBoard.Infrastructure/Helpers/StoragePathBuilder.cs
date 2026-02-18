using JobBoard.Data.enums;

namespace JobBoard.Infrastructure.Helpers
{
	public static class StoragePathBuilder
	{
		public static string Build<TId>(
			FileOwnerType resource,
			TId resourceId,
			string fileName,
			FilePathType pathType)
			where TId : notnull
		{
			var safeFileName = SanitizeFileName(fileName);

			string basePath = $"{resource.ToString().ToLower()}/{resourceId}";

			return pathType switch
			{
				FilePathType.ResourceId => $"{basePath}/{resourceId}{Path.GetExtension(fileName)}",
				FilePathType.UuidFileName => $"{basePath}/{Guid.NewGuid()}_{safeFileName}",
				_ => throw new ArgumentOutOfRangeException(
					nameof(pathType),
					$"Unsupported FilePathType value: {pathType}")
			};


		}

		public static string GetOriginalFileName(string fullPath)
		{
			// Extract filename with extension
			var fileName = Path.GetFileName(fullPath);

			// Find first underscore
			var underscoreIndex = fileName.IndexOf('_');

			if (underscoreIndex >= 0)
			{
				return fileName.Substring(underscoreIndex + 1);
			}

			return fileName; // fallback if format is unexpected
		}


		private static string SanitizeFileName(string fileName)
		{
			return Path.GetFileName(fileName);
		}
	}

}
