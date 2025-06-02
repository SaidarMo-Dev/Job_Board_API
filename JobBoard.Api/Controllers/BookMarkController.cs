using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.BookMarks.Commands.Models;
using JobBoard.Core.Feutures.BookMarks.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{

	[ApiController]
	[Authorize(Roles = "User")]
	public class BookMarkController : AppControllerbase
	{
		[SwaggerOperation(Summary = "Get bookmark by ID",
				  Description = "Retrieves a specific bookmark using its unique identifier.",
				  OperationId = "GetBookmarkByID")]


		[HttpGet(Router.BookMarkRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetBookmarkByID([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetBookmarkByIdQuery { Id = Id }));

		}
		[SwaggerOperation(Summary = "Get paginated bookmarks (Admin only)",
				  Description = "Returns a paginated list of all bookmarks. Requires admin role.",
				  OperationId = "BookmarksPaginate")]

		[Authorize(Roles = "Admin")]
		[HttpGet(Router.BookMarkRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> BookmarksPaginate([FromQuery] GetPaginatedBookmarkListQuery request)
		{
			return Ok(await Mediator.Send(request));

		}



		[SwaggerOperation(Summary = "Get bookmarks by user ID",
				  Description = "Retrieves all bookmarks associated with a specific user.",
				  OperationId = "UserBookmarks")]

		[HttpGet(Router.ApplicationUserRoute.Bookmarks)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UserBookmarks([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetUserBookmarksQuery { UserId = Id }));
		}



		[SwaggerOperation(Summary = "Add new bookmark",
				  Description = "Creates a new bookmark entry in the system.",
				  OperationId = "AddBookMark")]

		[AllowAnonymous]
		[HttpPost(Router.BookMarkRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> AddBookMark([FromBody] AddBookMarkCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}



		[SwaggerOperation(Summary = "Delete bookmark by ID",
				  Description = "Deletes a specific bookmark by its unique identifier.",
				  OperationId = "DeleteBookMark")]

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
