using FluentValidation;
using JobBoard.Core.Common.Helpers;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Core.Helpers;
using JobBoard.Service.Abstractions;

namespace JobBoard.Core.Feutures.Companies.Commands.Validation
{
	public class AddCompanyCommandValidator : AbstractValidator<AddCompanyCommand>
	{
		private readonly ICompanyService _companyService;

		public AddCompanyCommandValidator(ICompanyService companyService)
		{
			_companyService = companyService;
			AddValidations();
			AddCustomValidation();

		}

		public void AddValidations()
		{

			RuleFor(x => x.CompanyName)
				.NotEmpty()
				.MaximumLength(200);

			RuleFor(x => x.Slug)
				.MaximumLength(150);

			RuleFor(x => x.ShortDescription)
				.NotEmpty()
				.MaximumLength(500);

			RuleFor(x => x.Description)
				.MaximumLength(1000);

			RuleFor(x => x.FoundedYear)
				.InclusiveBetween(1800, DateTime.Now.Year)
				.When(x => x.FoundedYear.HasValue);

			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();


		}
		public void AddCustomValidation()
		{

			RuleFor(x => x.Slug)
				.CustomAsync(async (slug, context, cancellationToken) =>
				{
					// Determine which value to use
					var rawValue = string.IsNullOrWhiteSpace(slug)
						? context.InstanceToValidate.CompanyName
						: slug;

					var normalizedSlug = SlugHelper.Normalize(rawValue);

					// Check for empty/invalid result
					if (string.IsNullOrWhiteSpace(normalizedSlug))
					{
						context.AddFailure("Slug", "Could not generate a valid slug from the name or slug provided.");
						return;
					}

					// Check for uniqueness in DB
					var exists = await _companyService.IsSlugExist(normalizedSlug);
					if (exists)
					{
						context.AddFailure("Slug", "This slug (or the one generated from the company name) is already taken.");
					}
				});

			// Uniqueness Checks
			RuleFor(x => x.CompanyName)
				.MustAsync(async (name, token) => !await _companyService.IsExistByNameAsync(name))
				.WithMessage("Company Name already exists.");


			// URL Validations
			RuleFor(x => x.WebsiteUrl)
				.Must(Util.IsValideUrl).WithMessage("Invalid Website URL.");

			RuleFor(x => x.LinkedInUrl)
				.Must(Util.IsValideUrl).When(x => !string.IsNullOrEmpty(x.LinkedInUrl))
				.WithMessage("Invalid LinkedIn URL.");


			RuleFor(x => x.PhoneNumber)
				.Must(key => string.IsNullOrEmpty(key) ? true : Util.IsValidePhoneNumber(key))
				.WithMessage("{PropertyName} is invalid");

			RuleFor(x => x.Fax)
				.Must(key => string.IsNullOrEmpty(key) ? true : Util.IsValidePhoneNumber(key))
				.WithMessage("{PropertyName} is invalid");



			// Phone/Fax Validations
			RuleFor(x => x.PhoneNumber)
				.Must(Util.IsValidePhoneNumber).When(x => !string.IsNullOrEmpty(x.PhoneNumber));

			RuleFor(x => x.Fax)
				.Must(Util.IsValidePhoneNumber).When(x => !string.IsNullOrEmpty(x.Fax));

		}

	}
}
