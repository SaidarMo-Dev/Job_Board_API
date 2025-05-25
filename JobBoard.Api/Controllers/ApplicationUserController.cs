using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,User")]
	public class ApplicationUserController : AppControllerbase
	{
		// paginate users
		[Authorize(Roles = "Admin")]
		[HttpGet(Router.ApplicationUserRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> PaginateUsers([FromQuery] GetPaginatedListUsersQuery query)
		{
			return Ok(await Mediator.Send(query));
		}

		// get user by Id
		[HttpGet(Router.ApplicationUserRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> FindById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetUserByIdQuery(Id)));
		}

		// create user
		[HttpPost(Router.ApplicationUserRoute.Register)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		// any one can create user 
		[AllowAnonymous]
		public async Task<IActionResult> CreateUser([FromBody] AddUserCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		// Update user
		[HttpPut(Router.ApplicationUserRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpPut(Router.ApplicationUserRoute.ChangePassword)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdatePassword([FromBody] ChangeUserPasswordCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


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
