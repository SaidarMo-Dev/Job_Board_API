using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Emails.commands.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	public class EmailController : AppControllerbase
	{
		[AllowAnonymous]


		[SwaggerOperation(
			Summary = "Send an Email",
			Description = "This EndPoint Send´s Email",
			OperationId = "SendEmail")]

		[HttpPost(Router.EmailRoute.SendEmail)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand req)
		{
			return NewResult(await Mediator.Send(req));
		}


	}
}
