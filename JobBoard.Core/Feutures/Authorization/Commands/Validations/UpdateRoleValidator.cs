using FluentValidation;
using JobBoard.Core.Feutures.Authorization.Commands.Models;

namespace JobBoard.Core.Feutures.Authorization.Commands.Validations
{
	public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
	{
		public UpdateRoleValidator()
		{
			ApplyValidations();
		}

		public void ApplyValidations()
		{
			RuleFor(x => x.RoleName)
				.NotNull().WithMessage("{PropertyName} Cannot be Nulll")
				.NotEmpty().WithMessage("{PropertyName} Cannot be Empty");

		}
	}
}
