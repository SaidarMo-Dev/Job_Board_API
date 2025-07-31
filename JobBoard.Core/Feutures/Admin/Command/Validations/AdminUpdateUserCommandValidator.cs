using FluentValidation;
using JobBoard.Core.Feutures.Admin.Command.Models;
using JobBoard.Core.Helpers;

namespace JobBoard.Core.Feutures.Admin.Command.Validations
{
	public class AdminUpdateUserValidator : AbstractValidator<AdminUpdateUserCommand>
	{
		public AdminUpdateUserValidator()
		{
			ApplyValidations();

		}

		public void ApplyValidations()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("{PropertyName} cannot be empty")
				.NotNull().WithMessage("{PropertyName} cannot be null");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("{PropertyName} cannot be empty")
				.NotNull().WithMessage("{PropertyName} cannot be null");


			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("{PropertyName} cannot be empty")
				.NotNull().WithMessage("{PropertyName} cannot be null")
				.Must(key => Util.IsValideEmail(key))
				.WithMessage("Invalide email");


			RuleFor(x => x.Role)
				.NotEmpty().WithMessage("{PropertyName} cannot be empty")
				.NotNull().WithMessage("{PropertyName} cannot be null");

		}
	}
}
