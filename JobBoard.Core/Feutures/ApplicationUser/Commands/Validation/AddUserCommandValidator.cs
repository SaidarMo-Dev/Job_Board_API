using FluentValidation;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Core.Helpers;
using JobBoard.Service.Abstractions;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Validation
{
	public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
	{
		private readonly ICountryService _countryService;

		public AddUserCommandValidator(ICountryService countryService)
		{
			_countryService = countryService;
			AddValidations();

		}

		public void AddValidations()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("{PropertyName} Cannot be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot be Null");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("{PropertyName} Cannot be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot be Null");


			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("{PropertyName} Cannot be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot be Null")
				.Matches(x => x.ConfirmPassword).WithMessage("Password And ConfirmPassword Does not Match!");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("{PropertyName} Cannot be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot be Null")
				.Must(key => Util.IsValideEmail(key))
				.WithMessage("Invalide Email");


		}
	}
}
