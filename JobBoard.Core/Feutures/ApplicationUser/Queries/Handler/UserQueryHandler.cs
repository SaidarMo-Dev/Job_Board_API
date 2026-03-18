using AutoMapper;
using JobBoard.Core.Authorization.Policies;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Models;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using JobBoard.Core.Helpers;
using JobBoard.Core.Resources;
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
									IRequestHandler<GetPaginatedListUsersQuery, PaginatedResponse<GetPaginatedListUsersQueryResponse>>,
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
		private readonly IFileStorageService _storageService;
		private readonly IFileResourceService _fileResourceService;


		#endregion

		#region Constructors

		public UserQueryHandler(UserManager<User> userManager,
								IMapper mapper,
								IUserService userService,
								IBookmarkService bookmarkService,
								IStringLocalizer<SharedResources> stringLocalizer,
								IAuthorizationService authorizationService,
								IHttpContextAccessor httpContextAccessor,
								ICurrentUserService currentUserService,
								IFileStorageService StorageService,
								IFileResourceService fileResourceService)

								: base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
			_userService = userService;
			_bookmarkService = bookmarkService;
			_authorizationService = authorizationService;
			_httpContextAccessor = httpContextAccessor;
			_currentUserservice = currentUserService;
			_storageService = StorageService;
			_fileResourceService = fileResourceService;
		}

		#endregion

		#region Handle Methods
		public async Task<Response<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await _userService.GetUserInfoByIdWithInclude(request.Id);

			if (user == null) return NotFound<GetUserByIdQueryResponse>();

			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserservice.GetCurrentUserPrincipal(),
				user,
				AuthorizationPolicies.SameUser);

			if (!isAuthorized.Succeeded)
				return Forbidden<GetUserByIdQueryResponse>();


			var UserResponse = _mapper.Map<GetUserByIdQueryResponse>(user);

			return Success(UserResponse);
		}

		public async Task<PaginatedResponse<GetPaginatedListUsersQueryResponse>> Handle(GetPaginatedListUsersQuery request, CancellationToken cancellationToken)
		{
			var users = _userManager.Users.AsQueryable();

			var usersResponse = await _mapper.ProjectTo<GetPaginatedListUsersQueryResponse>(users).ToPaginatedAsync(request.PageNumber, request.PageSize);

			return usersResponse;

		}

		public async Task<Response<GetCurrentUserQueryResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
		{

			// Retrieve the current user's ID from JWT claims
			var userId = _httpContextAccessor.HttpContext?.User
						.FindFirst(JwtClaimTypes.UserId)?.Value;

			// Validate the user ID claim and ensure it can be parsed to an integer
			if (!int.TryParse(userId, out var userIdInt))
				return BadRequest<GetCurrentUserQueryResponse>("Invalid userId claim.");

			// Fetch the user from the database, including related Country information
			var user = await _userManager.Users
						.Include(x => x.Country)
						.FirstOrDefaultAsync(x => x.Id == userIdInt);

			if (user is null)
				return NotFound<GetCurrentUserQueryResponse>("User not Found");

			// Check Ownership
			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserservice.GetCurrentUserPrincipal(),
				user,
				AuthorizationPolicies.SameUser);

			if (!isAuthorized.Succeeded)
				return Forbidden<GetCurrentUserQueryResponse>("Access denied");

			// Map the user entity to the response DTO
			var userResponse = _mapper.Map<GetCurrentUserQueryResponse>(user);

			// If the user has a profile image, fetch the corresponding FileResource
			// and generate a signed URL for secure access
			if (user.ProfileImageFileId is int fileId)
			{
				var file = await _fileResourceService.GetByIdAsync(fileId);
				if (file != null)
				{
					try
					{
						userResponse.ProfileImageUrl = await _storageService.CreateSignedReadUrlAsync(
						_storageService.GetBucket(file.OwnerType),
						file.Path);

					}
					catch (Exception ex)
					{
						// TODO : Log error into error file
					}
				}
			}

			// Return the final user response, including the signed URL for the profile image if available
			return Success(userResponse);

		}
		public async Task<Response<GetUserDashboardStatsQueryResponse>> Handle(GetUserDashboardStatsQuery request, CancellationToken cancellationToken)
		{
			var user = _currentUserservice.GetCurrentUser();

			if (user.Id != request.Id) return Forbidden<GetUserDashboardStatsQueryResponse>("You don't have access to perform this operation!");

			var stats = await _userService.GetUserDashboardStatsAsync(request.Id);

			if (stats is null) return Success(new GetUserDashboardStatsQueryResponse());


			return Success(new GetUserDashboardStatsQueryResponse
			{
				TotalSavedJobs = stats.TotalSavedJobs,
				TotalApplications = stats.TotalApplications,
				Pending = stats.Pending,
				Rejected = stats.Rejected,
				ProfileCompletion = Util.CalculateProfileCompletion(user)

			});

		}



		#endregion

	}
}
