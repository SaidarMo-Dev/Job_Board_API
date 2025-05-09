using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Authentication.Commands.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	public class AuthenticationController : AppControllerbase
	{
		[HttpPost(Router.AuthenticationRoute.SignIn)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> SignIn([FromForm] SignInCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[HttpPost(Router.AuthenticationRoute.RefreshToken)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> RefreshNewToken([FromForm] RefreshNewAccessToken request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpPut(Router.AuthenticationRoute.ConfirmEmail)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}
	}
}
