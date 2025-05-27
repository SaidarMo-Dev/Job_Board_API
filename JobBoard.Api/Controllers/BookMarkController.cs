using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.BookMarks.Commands.Models;
using JobBoard.Core.Feutures.BookMarks.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{

	[ApiController]
	[Authorize(Roles = "User")]
	public class BookMarkController : AppControllerbase
	{


		[HttpGet(Router.BookMarkRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetBookmarkByID([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetBookmarkByIdQuery { Id = Id }));

		}

		[Authorize(Roles = "Admin")]
		[HttpGet(Router.BookMarkRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> BookmarksPaginate([FromQuery] GetPaginatedBookmarkListQuery request)
		{
			return Ok(await Mediator.Send(request));

		}


		[HttpGet(Router.ApplicationUserRoute.Bookmarks)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UserBookmarks([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetUserBookmarksQuery { UserId = Id }));
		}



		[AllowAnonymous]
		[HttpPost(Router.BookMarkRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> AddBookMark([FromBody] AddBookMarkCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[AllowAnonymous]
		[HttpDelete(Router.BookMarkRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteBookMark([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteBookmarkByIdCommand { Id = Id }));

		}
	}
}
