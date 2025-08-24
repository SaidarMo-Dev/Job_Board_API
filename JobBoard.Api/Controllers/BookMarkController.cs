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
	[Authorize]
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



		[SwaggerOperation(Summary = "Get user bookmarks",
				  Description = "Retrieves all bookmarks associated with a specific user.",
				  OperationId = "UserBookmarks")]

		[HttpGet(Router.ApplicationUserRoute.Bookmarks)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UserBookmarks([FromQuery] GetUserBookmarksQuery request)
		{
			return Ok(await Mediator.Send(request));
		}



		[SwaggerOperation(Summary = "Add new bookmark",
				  Description = "Creates a new bookmark entry in the system.",
				  OperationId = "AddBookMark")]

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

		[HttpDelete(Router.BookMarkRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteBookMark([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteBookmarkByIdCommand { Id = Id }));

		}

		[SwaggerOperation(Summary = "Delete bookmark by job ID",
			  Description = "Deletes a specific bookmark by its job Id.",
			  OperationId = "DeleteBookMarkByJobId")]

		[HttpDelete(Router.BookMarkRoute.DeleteByJobId)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteBookMarkByJobId([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteBookmarkByJobIdCommand(Id)));

		}



		[SwaggerOperation(Summary = "Get user bookmarks",
				  Description = "Retrieves all bookmarks associated with a specific user.",
				  OperationId = "TotalUserBookmarks")]

		[HttpGet(Router.ApplicationUserRoute.TotaleBookmarks)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> TotalUserBookmarks([FromQuery] GetUserSavedJobsCount request)
		{
			return Ok(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Get user Saved job ids",
			  Description = "Retrieves all saved job ids associated with a specific user.",
			  OperationId = "GetSavedJobIds")]

		[HttpGet(Router.BookMarkRoute.UserSavedJobIds)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetUserSavedJobIds([FromRoute] int Id)
		{
			return Ok(await Mediator.Send(new GetSavedJobIdsQuery(Id)));
		}

		[Authorize]
		[SwaggerOperation(Summary = "Get recent user Saved job",
		  Description = "Retrieves recent saved jobs associated with a specific user.",
		  OperationId = "GetRecentSavedJobs")]

		[HttpGet(Router.BookMarkRoute.RecentSavedJobs)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetRecentSavedJobs([FromQuery] GetRecentSavedJobsQuery query)
		{
			return Ok(await Mediator.Send(query));
		}


	}
}
