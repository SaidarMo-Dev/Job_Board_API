using FluentValidation;
using JobBoard.Core.Feutures.Authentication.Commands.Models;
using JobBoard.Core.Helpers;

namespace JobBoard.Core.Feutures.Authentication.Commands.Validations
{
	public class AddRecoveryContactValidation : AbstractValidator<AddRecoveryContactCommand>
	{
		public AddRecoveryContactValidation()
		{
			AddValidations();
			AddCustomValidation();
		}

		public void AddValidations()
		{

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");

			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");
		}

		public void AddCustomValidation()
		{
			RuleFor(x => x.Email).Must((key) => Util.IsValideEmail(key))
				.WithMessage("Invalide email address");
			RuleFor(x => x.PhoneNumber).Must((key) => Util.IsValidePhoneNumber(key))
				.WithMessage("Invalide Phone number");
		}
	}
}