using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Models;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Handler
{
	public class UserQueryHandler : ResponseHandler,
									IRequestHandler<GetUserByIdQuery, Response<GetUserByIdQueryResponse>>,
									IRequestHandler<GetPaginatedListUsersQuery, PaginatedResponse<List<GetPaginatedListUsersQueryResponse>>>,
									IRequestHandler<GetUserBookmarksQuery, Response<List<GetUseBookmarksQueryResponse>>>
	{

		#region Fields

		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IBookmarkService _bookmarkService;

		#endregion

		#region Constructors

		public UserQueryHandler(UserManager<User> userManager,
								IMapper mapper,
								IUserService userService,
								IBookmarkService bookmarkService,
								IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
			_userService = userService;
			_bookmarkService = bookmarkService;
		}

		#endregion

		#region Handle Methods
		public async Task<Response<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var User = await _userService.GetUserInfoByIdWithEnclude(request.Id);

			if (User == null) return NotFound<GetUserByIdQueryResponse>();

			var UserResponse = _mapper.Map<GetUserByIdQueryResponse>(User);

			return Success(UserResponse);
		}

		public async Task<PaginatedResponse<List<GetPaginatedListUsersQueryResponse>>> Handle(GetPaginatedListUsersQuery request, CancellationToken cancellationToken)
		{
			var users = _userManager.Users.AsQueryable();

			var usersResponse = await _mapper.ProjectTo<GetPaginatedListUsersQueryResponse>(users).ToPaginatedAsync(request.PageNumber, request.PageSize);

			return usersResponse;

		}

		public async Task<Response<List<GetUseBookmarksQueryResponse>>> Handle(GetUserBookmarksQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user is null) return NotFound<List<GetUseBookmarksQueryResponse>>();

			var bookmarks = await _bookmarkService.GetUserBookmarks(request.UserId);

			return Success(_mapper.Map<List<GetUseBookmarksQueryResponse>>(bookmarks));
		}


		#endregion

	}
}
