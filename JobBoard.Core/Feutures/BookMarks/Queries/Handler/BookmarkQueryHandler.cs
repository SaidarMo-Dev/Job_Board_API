using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Queries.Models;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Handler
{
	public class BookmarkQueryHandler : ResponseHandler,
				IRequestHandler<GetBookmarkByIdQuery, Response<GetBookmarkByIdQueryResponse>>,
				IRequestHandler<GetPaginatedBookmarkListQuery, PaginatedResponse<List<GetPaginatedBookmarkListQueryResponse>>>,
				IRequestHandler<GetUserBookmarksQuery, Response<GetUserBookmarksQueryResponse>>
	{
		#region Fields
		private readonly IBookmarkService _bookmarkService;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		#endregion

		#region Constructors
		public BookmarkQueryHandler(IBookmarkService bookmarkService,
									IMapper mapper,
									IStringLocalizer<SharedResources> stringLocalizer,
									UserManager<User> userManager
									) : base(stringLocalizer)
		{
			_bookmarkService = bookmarkService;
			_mapper = mapper;
			_userManager = userManager;
		}
		#endregion


		#region Handle Methods
		public async Task<Response<GetBookmarkByIdQueryResponse>> Handle(GetBookmarkByIdQuery request, CancellationToken cancellationToken)
		{
			var Bookmark = await _bookmarkService.GetBookmarkByIdWithIncludeAsync(request.Id);
			if (Bookmark == null) return NotFound<GetBookmarkByIdQueryResponse>();

			var BookmarkMapping = _mapper.Map<GetBookmarkByIdQueryResponse>(Bookmark);

			return Success(BookmarkMapping);


		}

		public async Task<PaginatedResponse<List<GetPaginatedBookmarkListQueryResponse>>> Handle(GetPaginatedBookmarkListQuery request, CancellationToken cancellationToken)
		{
			var ListBookmarksQueryable = _bookmarkService.GetBookmarksQueryable();

			var BookmarksPaginatedResult = await _mapper.ProjectTo<GetPaginatedBookmarkListQueryResponse>(ListBookmarksQueryable)
												.ToPaginatedAsync(request.Page, request.Size);

			return BookmarksPaginatedResult;
		}

		public async Task<Response<GetUserBookmarksQueryResponse>> Handle(GetUserBookmarksQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user is null) return NotFound<GetUserBookmarksQueryResponse>();

			var bookmarks = await _bookmarkService.GetUserBookmarks(request.UserId);

			if (bookmarks is null) return BadRequest<GetUserBookmarksQueryResponse>("No Bookmarks found");

			var bookmarksDto = _mapper.Map<List<BookmarkResponse>>(bookmarks);

			return Success(new GetUserBookmarksQueryResponse { Bookmarks = bookmarksDto });

		}

		#endregion

	}
}
