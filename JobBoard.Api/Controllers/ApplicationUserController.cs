using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class ApplicationUserController : AppControllerbase
	{
		// paginate users
		[SwaggerOperation(Summary = "Paginate users",
				  Description = "Retrieves a paginated list of users based on the given filter and pagination parameters.",
				  OperationId = "PaginateUsers")]

		[HttpGet(Router.ApplicationUserRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> PaginateUsers([FromQuery] GetPaginatedListUsersQuery query)
		{
			return Ok(await Mediator.Send(query));
		}

		// get user by Id


		[SwaggerOperation(Summary = "Get user by ID",
						  Description = "Retrieves a single user by their unique identifier.",
						  OperationId = "GetUserById")]

		[AllowAnonymous]
		[HttpGet(Router.ApplicationUserRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> FindById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetUserByIdQuery(Id)));
		}

		// create user

		[SwaggerOperation(Summary = "Register user",
						  Description = "Creates a new user account with the provided information.",
						  OperationId = "RegisterUser")]

		[AllowAnonymous]
		[HttpPost(Router.ApplicationUserRoute.Register)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public async Task<IActionResult> CreateUser([FromBody] AddUserCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		// Update user

		[SwaggerOperation(Summary = "Update user",
						  Description = "Updates the information of an existing user.",
						  OperationId = "UpdateUser")]

		[AllowAnonymous]
		[HttpPut(Router.ApplicationUserRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Change user password",
						  Description = "Changes the password of an existing user account.",
						  OperationId = "ChangeUserPassword")]

		[AllowAnonymous]
		[HttpPut(Router.ApplicationUserRoute.ChangePassword)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Delete user",
						  Description = "Deletes a user account by its unique identifier.",
						  OperationId = "DeleteUserById")]

		[AllowAnonymous]
		[HttpDelete(Router.ApplicationUserRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteUser([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteUserCommand { Id = Id }));
		}

	}
}
