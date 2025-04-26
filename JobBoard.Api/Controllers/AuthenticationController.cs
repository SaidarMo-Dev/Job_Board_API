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

	}
}
