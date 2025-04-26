using FluentValidation;
using JobBoard.Core.Feutures.Authorization.Commands.Models;
using JobBoard.Service.Authorization;

namespace JobBoard.Core.Feutures.Authorization.Commands.Validations
{
	public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
	{
		private readonly IAuthorizationService _authorizationService;

		public AddRoleCommandValidator(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
			ApplyValidations();
		}

		public void ApplyValidations()
		{
			RuleFor(x => x.RoleName)
				.NotEmpty().WithMessage("{PropertyName} Cannot be Empty")
				.NotNull().WithMessage("{PropertyName} Cannot be Null")
				.MustAsync(async (key, cancellationToken) => !await _authorizationService.IsRoleExitsAsync(key))
				.WithMessage("RoleName already Exists");

		}
	}


}
