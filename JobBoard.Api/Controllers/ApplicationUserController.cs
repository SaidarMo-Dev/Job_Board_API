using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	public class ApplicationUserController : AppControllerbase
	{
		// paginate users
		[HttpGet(Router.ApplicationUserRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> PaginateUsers([FromQuery] GetPaginatedListUsersQuery query)
		{
			return Ok(await Mediator.Send(query));
		}

		// get user by Id
		[HttpGet(Router.ApplicationUserRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> FindById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetUserByIdQuery(Id)));
		}

		// create user
		[HttpPost(Router.ApplicationUserRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateUser([FromBody] AddUserCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		// Update user
		[HttpPut(Router.ApplicationUserRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


	}
}
