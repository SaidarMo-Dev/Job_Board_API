using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.Files.Validations
{
	public static class FileValidatorExtensions
	{
		public static IRuleBuilderOptions<T, IFormFile> NotEmptyFile<T>(
			this IRuleBuilder<T, IFormFile> ruleBuilder)
			=> ruleBuilder.Must(FileValidationRules.NotEmpty)
				.WithMessage("File cannot be empty");

		public static IRuleBuilderOptions<T, IFormFile> WithMaxSize<T>(
			this IRuleBuilder<T, IFormFile> ruleBuilder, long maxSize)
			=> ruleBuilder.Must(f => FileValidationRules.HasMaxSize(f, maxSize))
				.WithMessage($"File must be less than {maxSize} bytes");

		public static IRuleBuilderOptions<T, IFormFile> WithAllowedExtensions<T>(
			this IRuleBuilder<T, IFormFile> ruleBuilder, IEnumerable<string> extensions)
			=> ruleBuilder.Must(f => FileValidationRules.HasAllowedExtension(f, extensions))
				.WithMessage("File extension is not allowed");

		public static IRuleBuilderOptions<T, IFormFile> WithAllowedContentTypes<T>(
			this IRuleBuilder<T, IFormFile> ruleBuilder, IEnumerable<string> contentTypes)
			=> ruleBuilder.Must(f => FileValidationRules.HasAllowedContentType(f, contentTypes))
				.WithMessage("File type is not allowed");

		public static IRuleBuilderOptions<T, IFormFile> HasSafeFileName<T>(
			this IRuleBuilder<T, IFormFile> ruleBuilder)
			=> ruleBuilder.Must(FileValidationRules.HasSafeFileName)
				.WithMessage("File name is unsafe or too long");

		public static IRuleBuilderOptions<T, IFormFile> SingleExtension<T>(
			this IRuleBuilder<T, IFormFile> ruleBuilder)
			=> ruleBuilder.Must(FileValidationRules.HasValidSingleExtension)
				.WithMessage("File has multiple extensions");

		public static IRuleBuilderOptions<T, IFormFile> OnlyImages<T>(
			this IRuleBuilder<T, IFormFile> ruleBuilder)
			=> ruleBuilder.Must(FileValidationRules.IsImageFile)
				.WithMessage("Only image files are allowed");

		public static IRuleBuilderOptions<T, IFormFile> OnlyPdf<T>(
			this IRuleBuilder<T, IFormFile> ruleBuilder)
			=> ruleBuilder.Must(FileValidationRules.IsPdfFile)
				.WithMessage("Only PDF files are allowed");
	}

}
