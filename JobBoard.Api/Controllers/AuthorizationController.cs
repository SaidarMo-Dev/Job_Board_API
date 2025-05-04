using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Authorization.Commands.Models;
using JobBoard.Core.Feutures.Authorization.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,SuperAdmin")]
	public class AuthorizationController : AppControllerbase
	{
		[HttpGet(Router.AuthorizationRoute.GetAllRoles)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetListRolesQuery()));
		}


		[HttpGet(Router.AuthorizationRoute.GetRoleById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetRoleById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleRoleQuery { Id = Id }));
		}

		// manage user roles
		[HttpGet(Router.AuthorizationRoute.ManageUserRoles)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> ManageUserRoles([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new ManageUserRolesQuery { UserId = Id }));
		}

		[HttpPost(Router.AuthorizationRoute.Create)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> AddRole([FromForm] AddRoleCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpPut(Router.AuthorizationRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpDelete(Router.AuthorizationRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteRole([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteRoleCommand { Id = Id }));
		}
	}
}
