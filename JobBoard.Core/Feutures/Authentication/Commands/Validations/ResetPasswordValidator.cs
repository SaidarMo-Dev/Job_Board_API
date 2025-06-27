using FluentValidation;
using JobBoard.Core.Feutures.Authentication.Commands.Models;
using JobBoard.Core.Resources;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Authentication.Commands.Validations
{
	public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
	{
		#region Fields 
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;


		#endregion
		public ResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;

			ApplyValidations();

		}

		public void ApplyValidations()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotNull]);

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotNull]);

			RuleFor(x => x.ConfirmPassword)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotNull]);


			RuleFor(x => x.Password)
				.Matches(x => x.ConfirmPassword)
				.WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordsNotMatches]);

		}
	}
}
