using JobBoard.Api.Bases;
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
		public async Task<IActionResult> RefreshNewToken([FromForm] RefreshNewAccessToken request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Confirm email",
				  Description = "Confirms a user's email address using a confirmation code or token.",
				  OperationId = "ConfirmEmail")]

		[HttpGet(Router.AuthenticationRoute.ConfirmEmail)]
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

	}
}
