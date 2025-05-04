using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Queries.Models;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Handler
{
	public class BookmarkQueryHandler : ResponseHandler,
				IRequestHandler<GetBookmarkByIdQuery, Response<GetBookmarkByIdQueryResponse>>,
				IRequestHandler<GetPaginatedBookmarkListQuery, PaginatedResponse<List<GetPaginatedBookmarkListQueryResponse>>>
	{
		#region Fields
		private readonly IBookmarkService _bookmarkService;
		private readonly IMapper _mapper;

		#endregion

		#region Constructors
		public BookmarkQueryHandler(IBookmarkService bookmarkService,
									IMapper mapper,
									IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_bookmarkService = bookmarkService;
			_mapper = mapper;
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

		#endregion

	}
}
