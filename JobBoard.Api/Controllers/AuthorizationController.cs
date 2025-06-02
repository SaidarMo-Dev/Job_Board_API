using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Authorization.Commands.Models;
using JobBoard.Core.Feutures.Authorization.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,SuperAdmin")]
	public class AuthorizationController : AppControllerbase
	{
		[SwaggerOperation(Summary = "Get all roles",
				  Description = "Returns a list of all roles defined in the system.",
				  OperationId = "GetAllRoles")]



		[HttpGet(Router.AuthorizationRoute.GetAllRoles)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetListRolesQuery()));
		}

		[SwaggerOperation(Summary = "Get role by ID",
				  Description = "Retrieves detailed information about a specific role by its ID.",
				  OperationId = "GetRoleById")]

		[HttpGet(Router.AuthorizationRoute.GetRoleById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetRoleById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleRoleQuery { Id = Id }));
		}

		//Get  manage user roles

		[SwaggerOperation(Summary = "Get user roles management data",
				  Description = "Fetches role assignment details for a specific user.",
				  OperationId = "GetManageUserRoles")]

		[HttpGet(Router.AuthorizationRoute.GetManageUserRoles)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetManageUserRoles([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new ManageUserRolesQuery { UserId = Id }));
		}

		// Update user roles


		[SwaggerOperation(Summary = "Update user roles",
				  Description = "Updates the roles assigned to a specific user.",
				  OperationId = "UpdateUserRoles")]

		[HttpPut(Router.AuthorizationRoute.UpdateUserRoles)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		// Add Role


		[SwaggerOperation(Summary = "Add new role",
				  Description = "Creates a new role in the system with the specified permissions.",
				  OperationId = "AddRole")]

		[HttpPost(Router.AuthorizationRoute.Create)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> AddRole([FromForm] AddRoleCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[SwaggerOperation(Summary = "Update role",
				  Description = "Updates the details of an existing role.",
				  OperationId = "UpdateRole")]

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


		[SwaggerOperation(Summary = "Delete role",
				  Description = "Deletes an existing role by ID.",
				  OperationId = "DeleteRole")]

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



		[SwaggerOperation(Summary = "Get user claims management data",
				  Description = "Fetches claim assignment details for a specific user.",
				  OperationId = "GetManageUserClaims")]

		[HttpGet(Router.AuthorizationRoute.ManageUserClaims)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> ManageUserClaims([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new ManageUserClaimsQuery { UserId = Id }));
		}

		[SwaggerOperation(Summary = "Update user claims",
				  Description = "Updates the claims assigned to a specific user.",
				  OperationId = "UpdateUserClaims")]

		[HttpPut(Router.AuthorizationRoute.UpdateUserClaims)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimCommand request)
		{

			return NewResult(await Mediator.Send(request));
		}

	}
}
