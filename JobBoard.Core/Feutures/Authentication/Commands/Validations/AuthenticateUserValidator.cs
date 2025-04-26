using FluentValidation;
using JobBoard.Core.Feutures.Authentication.Commands.Models;

namespace JobBoard.Core.Feutures.Authentication.Commands.Validations
{
	public class ChangePasswordValidator : AbstractValidator<SignInCommand>
	{
		public ChangePasswordValidator()
		{
			AddValidations();
		}

		public void AddValidations()
		{

			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");
		}
	}
}
