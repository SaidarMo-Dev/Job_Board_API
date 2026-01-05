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
	[Authorize]
	public class ApplicationUserController : AppControllerbase
	{
		// paginate users
		[Authorize(Roles = "Admin")]
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


		// get current user
		[SwaggerOperation(Summary = "Get current user",
						  Description = "Retrieves the current user Info.",
						  OperationId = "GetCurrentUser")]


		[HttpGet(Router.ApplicationUserRoute.me)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetCurrentUser()
		{
			return NewResult(await Mediator.Send(new GetCurrentUserQuery()));
		}


		// get user by Id

		[SwaggerOperation(Summary = "Get user by ID",
						  Description = "Retrieves a single user by their unique identifier.",
						  OperationId = "GetUserById")]

		[Authorize]
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


		[HttpPut(Router.ApplicationUserRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}



		[SwaggerOperation(Summary = "Delete user",
						  Description = "Deletes a user account by its unique identifier.",
						  OperationId = "DeleteUserById")]


		[HttpDelete(Router.ApplicationUserRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteUser([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteUserCommand { Id = Id }));
		}


		[SwaggerOperation(Summary = "get user dashboard stats",
					  Description = "get user stats like saved jobs total application etc...",
					  OperationId = "GetDashboardStats")]

		[HttpGet(Router.ApplicationUserRoute.DashboardStats)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetDashboardStats([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetUserDashboardStatsQuery(Id)));
		}


		// Upload user profile image

		[AllowAnonymous]
		[SwaggerOperation(Summary = "Upload profile image",
					  Description = "Upload user profile image",
					  OperationId = "UploadProfileImage")]

		[HttpPost(Router.ApplicationUserRoute.UploadProfileImage)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UploadUserProfileImage([FromRoute] int Id, [FromForm] UploadProfileImageRequest request)
		{
			return NewResult(await Mediator.Send(new SetUserProfileImageCommand { UserId = Id, ProfileImage = request.ProfileImage }));
		}
	}
}
