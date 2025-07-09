using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Models;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
using JobBoard.Core.Wrapers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Handler
{
	public class UserQueryHandler : ResponseHandler,
									IRequestHandler<GetUserByIdQuery, Response<GetUserByIdQueryResponse>>,
									IRequestHandler<GetPaginatedListUsersQuery, PaginatedResponse<List<GetPaginatedListUsersQueryResponse>>>,
									IRequestHandler<GetCurrentUserQuery, Response<GetCurrentUserQueryResponse>>,
									IRequestHandler<GetUserDashboardStatsQuery, Response<GetUserDashboardStatsQueryResponse>>

	{

		#region Fields

		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IBookmarkService _bookmarkService;
		private readonly IAuthorizationService _authorizationService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ICurrentUserService _currentUserservice;


		#endregion

		#region Constructors

		public UserQueryHandler(UserManager<User> userManager,
								IMapper mapper,
								IUserService userService,
								IBookmarkService bookmarkService,
								IStringLocalizer<SharedResources> stringLocalizer,
								IAuthorizationService authorizationService,
								IHttpContextAccessor httpContextAccessor,
								ICurrentUserService currentUserService)

								: base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
			_userService = userService;
			_bookmarkService = bookmarkService;
			_authorizationService = authorizationService;
			_httpContextAccessor = httpContextAccessor;
			_currentUserservice = currentUserService;
		}

		#endregion

		#region Handle Methods
		public async Task<Response<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await _userService.GetUserInfoByIdWithInclude(request.Id);

			var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), user, new SameUserRequirement());

			if (!isAuthorized.Succeeded) return Forbidden<GetUserByIdQueryResponse>();

			if (user == null) return NotFound<GetUserByIdQueryResponse>();

			var UserResponse = _mapper.Map<GetUserByIdQueryResponse>(user);

			return Success(UserResponse);
		}

		public async Task<PaginatedResponse<List<GetPaginatedListUsersQueryResponse>>> Handle(GetPaginatedListUsersQuery request, CancellationToken cancellationToken)
		{
			var users = _userManager.Users.AsQueryable();

			var usersResponse = await _mapper.ProjectTo<GetPaginatedListUsersQueryResponse>(users).ToPaginatedAsync(request.PageNumber, request.PageSize);

			return usersResponse;

		}

		public async Task<Response<GetCurrentUserQueryResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
		{

			var userId = _httpContextAccessor.HttpContext?.User
						.FindFirst(nameof(JwtClaimModel.UserId))?.Value;

			if (userId is null) return BadRequest<GetCurrentUserQueryResponse>("No Claim userId Found");

			var user = await _userManager.Users.Include(c => c.Country).FirstOrDefaultAsync();


			if (user is null) return NotFound<GetCurrentUserQueryResponse>("User not Found");
			var userResponse = _mapper.Map<GetCurrentUserQueryResponse>(user);

			return Success(userResponse);

		}

		public async Task<Response<GetUserDashboardStatsQueryResponse>> Handle(GetUserDashboardStatsQuery request, CancellationToken cancellationToken)
		{
			var curretUserId = _currentUserservice.GetCurrentUserId();

			if (!curretUserId.Equals(request.Id)) return Forbidden<GetUserDashboardStatsQueryResponse>("You don't have access to perform this operation!");

			var stats = await _userService.GetUserDashboardStatsAsync(request.Id);

			if (stats is null) return Success(new GetUserDashboardStatsQueryResponse());

			return Success(new GetUserDashboardStatsQueryResponse
			{
				TotalSavedJobs = stats.TotalSavedJobs,
				TotalApplications = stats.TotalApplications,
				Pending = stats.Pending,
				Rejected = stats.Rejected
			});

		}



		#endregion

	}
}
