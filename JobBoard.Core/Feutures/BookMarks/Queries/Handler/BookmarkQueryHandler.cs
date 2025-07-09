using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Queries.Models;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
using JobBoard.Core.Wrapers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Handler
{
	public class BookmarkQueryHandler : ResponseHandler,
				IRequestHandler<GetBookmarkByIdQuery, Response<GetBookmarkByIdQueryResponse>>,
				IRequestHandler<GetPaginatedBookmarkListQuery, PaginatedResponse<List<GetPaginatedBookmarkListQueryResponse>>>,
				IRequestHandler<GetUserBookmarksQuery, PaginatedResponse<List<GetUserBookmarksQueryResponse>>>,
				IRequestHandler<GetUserSavedJobsCount, Response<int>>,
				IRequestHandler<GetSavedJobIdsQuery, Response<GetSavedJobIdsQueryResponse>>

	{
		#region Fields
		private readonly IBookmarkService _bookmarkService;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly IAuthorizationService _authorizationService;

		#endregion

		#region Constructors
		public BookmarkQueryHandler(IBookmarkService bookmarkService,
									IMapper mapper,
									IStringLocalizer<SharedResources> stringLocalizer,
									UserManager<User> userManager,
									IAuthorizationService authorizationService
									) : base(stringLocalizer)
		{
			_bookmarkService = bookmarkService;
			_mapper = mapper;
			_userManager = userManager;
			_authorizationService = authorizationService;
		}
		#endregion


		#region Handle Methods
		public async Task<Response<GetBookmarkByIdQueryResponse>> Handle(GetBookmarkByIdQuery request, CancellationToken cancellationToken)
		{
			var Bookmark = await _bookmarkService.GetBookmarkByIdWithIncludeAsync(request.Id);

			var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), Bookmark, new UserBookmarkRequirement());

			if (!isAuthorized.Succeeded) return Forbidden<GetBookmarkByIdQueryResponse>();

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

		public async Task<PaginatedResponse<List<GetUserBookmarksQueryResponse>>> Handle(GetUserBookmarksQuery request, CancellationToken cancellationToken)
		{

			var queryable = _bookmarkService.GetUserBookmarksQueryable(request.UserId);

			var result = await _mapper.ProjectTo<GetUserBookmarksQueryResponse>(queryable).ToPaginatedAsync(request.page, request.pageSize);

			return result;

		}

		public async Task<Response<int>> Handle(GetUserSavedJobsCount request, CancellationToken cancellationToken)
		{
			var result = await _bookmarkService.GetUserSavedJobsCount(request.UserId);
			return Success(result);
		}

		public async Task<Response<GetSavedJobIdsQueryResponse>> Handle(GetSavedJobIdsQuery request, CancellationToken cancellationToken)
		{
			var result = await _bookmarkService.GetUserSavedJobIds(request.UserId);

			return Success(new GetSavedJobIdsQueryResponse { SavedJobIds = result });
		}

		#endregion

	}
}
