using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Admin.Query.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	//[Authorize(Roles = "Admin")]
	[ApiController]
	public class AdminController : AppControllerbase
	{


		[SwaggerOperation(Summary = "get all users",
					  Description = "get all users information with paginations...",
					  OperationId = "Admin/GetUsers")]

		[HttpGet(Router.AdminRoute.GetUsers)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
		{
			var response = await Mediator.Send(query);

			return Ok(response);
		}
	}
}
