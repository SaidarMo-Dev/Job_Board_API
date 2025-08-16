using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Admin.Command.Models;
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

		[SwaggerOperation(Summary = "get admin profile",
					  Description = "get admin profile info",
					  OperationId = "Admin/Profile")]

		[HttpGet(Router.AdminRoute.Profile)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetAdminProfile()
		{

			return Ok(await Mediator.Send(new GetAdminProfileQuery()));
		}

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


		[HttpPost(Router.AdminRoute.AddUser)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> AddNewUser([FromBody] AdminAddUserCommand command)
		{
			return NewResult(await Mediator.Send(command));
		}


		[HttpPut(Router.AdminRoute.UpdateUser)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UpdateUser([FromBody] AdminUpdateUserCommand command)
		{
			return NewResult(await Mediator.Send(command));
		}

		[SwaggerOperation(Summary = "get all jobs",
					  Description = "get all jobs with paginations...",
					  OperationId = "Admin/GetJobs")]

		[HttpGet(Router.AdminRoute.GetJobs)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetJobs([FromQuery] GetAdminJobsQuery query)
		{
			var response = await Mediator.Send(query);

			return Ok(response);
		}
	}
}
