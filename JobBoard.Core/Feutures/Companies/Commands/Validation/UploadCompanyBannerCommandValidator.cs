using FluentValidation;
using JobBoard.Core.Feutures.Companies.Commands.Models;

namespace JobBoard.Core.Feutures.Companies.Commands.Validation
{
	public class UploadCompanyBannerCommandValidator : AbstractValidator<UploadCompanyBannerCommand>
	{
		public UploadCompanyBannerCommandValidator()
		{
			RuleFor(x => x.CompanyId)
				.NotEmpty();

			RuleFor(x => x.File)
				.NotNull()
				.Must(file => file.Length <= 5 * 1024 * 1024).WithMessage("Banner size must be less than 5MB.")
				.Must(file =>
				{
					var ext = Path.GetExtension(file.FileName).ToLower();
					return new[] { ".jpg", ".jpeg", ".png", ".webp" }.Contains(ext);
				}).WithMessage("Invalid banner format. WebP, JPG, or PNG preferred.");

			RuleFor(x => x.File)
				.Must(file => file.ContentType.StartsWith("image/"))
				.WithMessage("The uploaded file is not a valid image.");

		}
	}
}
