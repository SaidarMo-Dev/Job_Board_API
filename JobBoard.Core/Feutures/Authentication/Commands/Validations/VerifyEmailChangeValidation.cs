using FluentValidation;
using JobBoard.Core.Feutures.Authentication.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Service.Abstractions;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Authentication.Commands.Validations
{
	public class VerifyEmailChangeValidation : AbstractValidator<VerifyEmailChangeCommand>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IUserService _userService;
		public VerifyEmailChangeValidation(IStringLocalizer<SharedResources> stringLocalizer, IUserService userService)
		{

			_stringLocalizer = stringLocalizer;
			_userService = userService;
			ApplyValidations();


		}

		public void ApplyValidations()
		{
			RuleFor(x => x.NewEmail)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotNull]);

			RuleFor(x => x.OldEmail)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotNull]);

			RuleFor(x => x.Code)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotNull]);

		}

		public void CustomValidation()
		{
			RuleFor(x => x.NewEmail)
				.MustAsync(async (email, cancellationToken) => !await _userService.IsEmailExistAsync(email))
				.WithMessage("Email already Exist");
		}
	}
}
