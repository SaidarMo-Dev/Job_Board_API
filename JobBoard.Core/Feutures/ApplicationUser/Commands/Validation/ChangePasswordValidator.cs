using FluentValidation;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Validation
{
	public class ChangePasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
	{
		public ChangePasswordValidator()
		{
			AddValidations();
		}

		public void AddValidations()
		{
			RuleFor(x => x.CurrentPassword)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");

			RuleFor(x => x.NewPassword)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");

			RuleFor(x => x.ConfirmPassword)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");
		}
	}
}
