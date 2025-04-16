using FluentValidation;
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
				.NotEmpty().WithMessage("{PropertyName} Cannot be Empty.")
				.NotNull().WithMessage("{PropertyName} cannot be Null");


			RuleFor(x => x.Location)
				.NotEmpty().WithMessage("{PropertyName} Cannot be Empty.")
				.NotNull().WithMessage("{PropertyName} cannot be Null");


			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("{PropertyName} : Cannot be Empty.")
				.NotNull().WithMessage("{PropertyName} cannot be Null");


			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("{PropertyName} : Cannot be Empty.")
				.NotNull().WithMessage("{PropertyName} cannot be Null");


			RuleFor(x => x.Fax)
				.NotEmpty().WithMessage("{PropertyName} : Cannot be Empty.")
				.NotNull().WithMessage("{PropertyName} cannot be Null");

		}
		public void AddCustomValidation()
		{
			RuleFor(x => x.CompanyName)
				.MustAsync(async (key, cancellationToken) => !await _companyService.IsExistByNameAsync(key))
			.WithMessage("{PropertyName} already Exist!");

			RuleFor(x => x.WebsiteUrl)
				.Must(key => Util.IsValideUrl(key))
				.WithMessage("{PropertyName} Is Invalide");


			RuleFor(x => x.Email)
				.Must(key => Util.IsValideEmail(key))
				.WithMessage("{PropertyName} Is Invalide");

			RuleFor(x => x.PhoneNumber)
				.Must(key => Util.IsValidePhoneNumber(key))
				.WithMessage("{PropertyName} Is Invalide");

			RuleFor(x => x.Fax)
				.Must(key => Util.IsValidePhoneNumber(key))
				.WithMessage("{PropertyName} Is Invalide");


		}

	}
}
