using JobBoard.Data.enums;

namespace JobBoard.Infrastructure.Helpers
{
	public static class StoragePathBuilder
	{
		public static string Build<TId>(
			StorageResourceEnum resource,
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


		private static string SanitizeFileName(string fileName)
		{
			return Path.GetFileName(fileName);
		}
	}

}
