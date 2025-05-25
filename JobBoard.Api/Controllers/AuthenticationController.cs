using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Authentication.Commands.Models;
using JobBoard.Core.Feutures.Authentication.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "User")]
	public class AuthenticationController : AppControllerbase
	{
		[AllowAnonymous]
		[HttpPost(Router.AuthenticationRoute.SignIn)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> SignIn([FromForm] SignInCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[AllowAnonymous]
		[HttpPost(Router.AuthenticationRoute.RefreshToken)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> RefreshNewToken([FromForm] RefreshNewAccessToken request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[AllowAnonymous]
		[HttpGet(Router.AuthenticationRoute.ConfirmEmail)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[HttpPut(Router.AuthenticationRoute.SendResetPassword)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[HttpGet(Router.AuthenticationRoute.ConfirmResetPassword)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery request)
		{
			return NewResult(await Mediator.Send(request));

		}


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
