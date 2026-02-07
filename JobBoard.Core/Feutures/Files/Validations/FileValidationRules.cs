namespace JobBoard.Core.Feutures.Files.Validations
{
	using System.IO;
	using Microsoft.AspNetCore.Http;

	public static class FileValidationRules
	{
		// File exists and not empty
		public static bool NotEmpty(IFormFile file) =>
			file is not null && file.Length > 0;

		// Max file size in bytes
		public static bool HasMaxSize(IFormFile file, long maxSize) =>
			file != null && file.Length <= maxSize;

		// Allowed file extensions
		public static bool HasAllowedExtension(IFormFile file, IEnumerable<string> allowedExtensions)
		{
			if (file == null) return false;
			var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();
			return !string.IsNullOrWhiteSpace(ext) && allowedExtensions.Contains(ext);
		}

		// Allowed content types (MIME)
		public static bool HasAllowedContentType(IFormFile file, IEnumerable<string> allowedContentTypes) =>
			file != null && allowedContentTypes.Contains(file.ContentType);

		// File name is safe and valid
		public static bool HasSafeFileName(IFormFile file)
		{
			if (file == null) return false;
			var name = file.FileName;
			return !string.IsNullOrWhiteSpace(name)
				   && name.Length <= 255
				   && !name.Any(c => Path.GetInvalidFileNameChars().Contains(c));
		}

		// Prevent double extensions (image.jpg.exe)
		public static bool HasValidSingleExtension(IFormFile file)
		{
			if (file == null) return false;
			var extCount = Path.GetFileName(file.FileName).Count(c => c == '.');
			return extCount <= 1;
		}

		// Only allow image files
		public static bool IsImageFile(IFormFile file) =>
			file != null && file.ContentType.StartsWith("image/");

		// Only allow PDF
		public static bool IsPdfFile(IFormFile file) =>
			file != null && file.ContentType == "application/pdf";
	}


}
