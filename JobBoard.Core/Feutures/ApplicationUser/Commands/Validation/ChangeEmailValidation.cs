using FluentValidation;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Validation
{
	public class ChangeEmailValidation : AbstractValidator<ChangeEmailCommand>
	{

		public void AddValidations()
		{
			RuleFor(x => x.CurrentEmail)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");

			RuleFor(x => x.NewEmail)
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot Be Null");

		}
	}
}
