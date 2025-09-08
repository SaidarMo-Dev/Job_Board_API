using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Authentication.commands.Models;
using JobBoard.Core.Feutures.Authentication.Commands.Models;
using JobBoard.Core.Feutures.Authentication.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[AllowAnonymous]
	public class AuthenticationController : AppControllerbase
	{
		[SwaggerOperation(Summary = "Sign in",
				  Description = "Authenticates a user using their credentials and returns an access token.",
				  OperationId = "SignIn")]

		[HttpPost(Router.AuthenticationRoute.SignIn)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> SignIn([FromForm] SignInCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[SwaggerOperation(Summary = "Send confirm email link",
			  Description = "Sends a confirm email link or code to the user's registered email address.",
			  OperationId = "SendConfirmEmailLink")]

		[HttpGet(Router.AuthenticationRoute.SendConfirmeEmail)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> SendConfirmEmail([FromQuery] int userId)
		{
			return NewResult(await Mediator.Send(new SendConfirmEmailQuery(userId)));

		}


		[SwaggerOperation(Summary = "Refresh token",
				  Description = "Generates a new access token using a valid refresh token.",
				  OperationId = "RefreshAccessToken")]

		[HttpPost(Router.AuthenticationRoute.RefreshToken)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> RefreshNewToken([FromBody] RefreshNewAccessToken request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Confirm email by url",
				  Description = "Confirms a user's email address using a confirmation code or token.",
				  OperationId = "ConfirmEmailByUrl")]

		[HttpGet(Router.AuthenticationRoute.ConfirmEmailByUrl)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery request)
		{
			return NewResult(await Mediator.Send(request));

		}


		[SwaggerOperation(Summary = "Send reset password link",
				  Description = "Sends a password reset link or code to the user's registered email address.",
				  OperationId = "SendResetPasswordLink")]

		[HttpPut(Router.AuthenticationRoute.SendResetPassword)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}



		[SwaggerOperation(Summary = "Confirm password reset",
				  Description = "Validates the password reset token or code before allowing password change.",
				  OperationId = "ConfirmResetPassword")]

		[HttpGet(Router.AuthenticationRoute.ConfirmResetPassword)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery request)
		{
			return NewResult(await Mediator.Send(request));

		}



		[SwaggerOperation(Summary = "Reset password",
				  Description = "Resets the user's password using the provided reset token and new password.",
				  OperationId = "ResetPassword")]

		[HttpPost(Router.AuthenticationRoute.ResetPassword)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]


		public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[SwaggerOperation(Summary = "Send Confirm Email Code",
			  Description = "Sends a confirm email code to the user's registered email address.",
			  OperationId = "SendConfirmEmailCode")]

		[HttpPut(Router.AuthenticationRoute.SendConfirmeEmailCode)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> SendConfirmeEmailCode([FromQuery] SendConfirmEmail request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[SwaggerOperation(Summary = "Send email change code",
			  Description = "Sends a confirm email code to the user's changed email address.",
			  OperationId = "SendEmailChange")]

		[HttpPut(Router.AuthenticationRoute.SendEmailChange)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> SendEmailChange([FromQuery] SendEmailChangeCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}


		[SwaggerOperation(Summary = "Verify email change",
			  Description = "Verify the new email and change the user email to the new one.",
			  OperationId = "VerifyEmailChange")]

		[HttpPut(Router.AuthenticationRoute.VerifyEmailChange)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> VerifyEmailChange([FromBody] VerifyEmailChangeCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[SwaggerOperation(Summary = "Change user password",
						  Description = "Changes the password of an existing user account.",
						  OperationId = "ChangeUserPassword")]

		[AllowAnonymous]
		[HttpPut(Router.AuthenticationRoute.ChangePassword)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[SwaggerOperation(Summary = "Add user recovery contact",
					  Description = "add a recovery contact informations including email and phone number to an existing user account.",
					  OperationId = "AddRecoveryContact")]

		[AllowAnonymous]
		[HttpPut(Router.AuthenticationRoute.AddRecoveryContact)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> AddRecoveryContact([FromBody] AddRecoveryContactCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Confirm email by code",
					  Description = "Confirms a user's email address using a confirmation code.",
					  OperationId = "ConfirmEmailByCode")]

		[AllowAnonymous]
		[HttpGet(Router.AuthenticationRoute.ConfirmEmailByCode)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> ConfirmEmailByCode([FromQuery] ConfirmEmailByCode request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Verify password",
					  Description = "Verify user's passwor.",
					  OperationId = "VerifyPassword")]

		[HttpGet(Router.AuthenticationRoute.VerfiyPassword)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> VerifyPassword([FromQuery] VerifyPasswordQuery query)
		{
			return NewResult(await Mediator.Send(query));
		}

		[SwaggerOperation(
			Summary = "Resend a verification code",
			Description = "This EndPoint send's verification code",
			OperationId = "ResendVerificationCode")]

		[HttpPut(Router.AuthenticationRoute.ResendVerificationCode)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> ResendVerificationCode([FromQuery] ResendVerificationCodeCommand command)
		{
			return NewResult(await Mediator.Send(command));
		}
	}
}
