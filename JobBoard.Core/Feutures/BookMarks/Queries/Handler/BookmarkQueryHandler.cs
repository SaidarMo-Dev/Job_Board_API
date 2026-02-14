using AutoMapper;
using JobBoard.Core.Authorization.Policies;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Queries.Models;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Handler
{
	public class BookmarkQueryHandler : ResponseHandler,
				IRequestHandler<GetBookmarkByIdQuery, Response<GetBookmarkByIdQueryResponse>>,
				IRequestHandler<GetPaginatedBookmarkListQuery, PaginatedResponse<List<GetPaginatedBookmarkListQueryResponse>>>,
				IRequestHandler<GetUserBookmarksQuery, PaginatedResponse<List<GetUserBookmarksQueryResponse>>>,
				IRequestHandler<GetUserSavedJobsCount, Response<int>>,
				IRequestHandler<GetSavedJobIdsQuery, Response<GetSavedJobIdsQueryResponse>>,
				IRequestHandler<GetRecentSavedJobsQuery, Response<List<GetRecentSavedJobsQueryResponse>>>

	{
		#region Fields
		private readonly IBookmarkService _bookmarkService;
		private readonly IMapper _mapper;
		private readonly IAuthorizationService _authorizationService;
		private readonly ICurrentUserService _currentUserService;

		#endregion

		#region Constructors
		public BookmarkQueryHandler(IBookmarkService bookmarkService,
									IMapper mapper,
									IStringLocalizer<SharedResources> stringLocalizer,

									IAuthorizationService authorizationService,
									ICurrentUserService currentUserService
									) : base(stringLocalizer)
		{
			_bookmarkService = bookmarkService;
			_mapper = mapper;
			_authorizationService = authorizationService;
			_currentUserService = currentUserService;
		}
		#endregion


		#region Handle Methods
		public async Task<Response<GetBookmarkByIdQueryResponse>> Handle(GetBookmarkByIdQuery request, CancellationToken cancellationToken)
		{
			var Bookmark = await _bookmarkService.GetBookmarkByIdWithIncludeAsync(request.Id);

			if (Bookmark == null) return NotFound<GetBookmarkByIdQueryResponse>();

			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserService.GetCurrentUserPrincipal(),
				Bookmark,
				AuthorizationPolicies.CanAccessOwnBookmarks);

			if (!isAuthorized.Succeeded)
				return Forbidden<GetBookmarkByIdQueryResponse>();

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
			var currentUserId = _currentUserService.GetCurrentUserId();

			var queryable = _bookmarkService.GetUserBookmarksQueryable(currentUserId);

			var result = await _mapper.ProjectTo<GetUserBookmarksQueryResponse>(queryable).ToPaginatedAsync(request.page, request.pageSize);

			return result;

		}

		public async Task<Response<int>> Handle(GetUserSavedJobsCount request, CancellationToken cancellationToken)
		{
			var result = await _bookmarkService.GetUserSavedJobsCount(_currentUserService.GetCurrentUserId());
			return Success(result);
		}

		public async Task<Response<GetSavedJobIdsQueryResponse>> Handle(GetSavedJobIdsQuery request, CancellationToken cancellationToken)
		{

			var result = await _bookmarkService.GetUserSavedJobIds(_currentUserService.GetCurrentUserId());

			return Success(new GetSavedJobIdsQueryResponse { SavedJobIds = result });
		}

		public async Task<Response<List<GetRecentSavedJobsQueryResponse>>> Handle(GetRecentSavedJobsQuery request, CancellationToken cancellationToken)
		{

			var queryableJobs = await _bookmarkService.GetRecentSavedJobs
						(_currentUserService.GetCurrentUserId(), request.Take).ToListAsync();

			return Success(_mapper.Map<List<GetRecentSavedJobsQueryResponse>>(queryableJobs));

		}

		#endregion

	}
}
