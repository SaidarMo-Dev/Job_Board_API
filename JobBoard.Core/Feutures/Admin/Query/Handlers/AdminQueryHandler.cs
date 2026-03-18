using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Admin.Query.Models;
using JobBoard.Core.Feutures.Admin.Query.Responses;
using JobBoard.Core.Helpers;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Responses;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Admin.Query.Handlers
{
	public class AdminQueryHandler : ResponseHandler,
			IRequestHandler<GetUsersQuery, PaginatedResponse<UserManagementResponse>>,
			IRequestHandler<GetAdminProfileQuery, Response<GetAdminProfileQueryResponse>>,
			IRequestHandler<GetAdminJobsQuery, PaginatedResponse<GetAdminJobsQueryResponse>>
	{
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ICurrentUserService _currentUserService;
		private readonly IJobService _jobService;
		#region Fields

		#endregion

		#region Constructors
		public AdminQueryHandler(IUserService userService,
								UserManager<User> userManager,
								IMapper mapper,
								IStringLocalizer<SharedResources> stringLocalizer,
								ICurrentUserService currentUserService,
								IJobService jobService
							) : base(stringLocalizer)
		{
			_userService = userService;
			_userManager = userManager;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
			_currentUserService = currentUserService;
			_jobService = jobService;
		}
		#endregion

		#region Handles
		public async Task<PaginatedResponse<UserManagementResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var users = _userService.GetUsersQueryable(request.Search, request.FilterByRole, request.FilterStatus);

			var usersDto = await users.ToPaginatedAsync(request.Page, request.Size);

			return usersDto;

		}

		public async Task<Response<GetAdminProfileQueryResponse>> Handle(GetAdminProfileQuery request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.GetCurrentUserId();

			var user = await _userService.GetAdminProfile(userId);
			if (user is null) return NotFound<GetAdminProfileQueryResponse>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

			// map the user into valide response

			var userResponse = _mapper.Map<GetAdminProfileQueryResponse>(user);
			userResponse.Roles = (await _userManager.GetRolesAsync(user)).ToArray();

			return Success(userResponse);

		}

		public async Task<PaginatedResponse<GetAdminJobsQueryResponse>> Handle(GetAdminJobsQuery request, CancellationToken cancellationToken)
		{
			var jobsQueryable = _jobService.GetJobsQueryable();
			jobsQueryable = jobsQueryable.ApplySearch(request.Search);
			jobsQueryable = jobsQueryable.FilterJobs(request.JobStatus, request.Categories,
									request.Locations, request.Companies, request.From, request.To);

			var jobs = await _mapper.ProjectTo<GetAdminJobsQueryResponse>(jobsQueryable)
				.ToPaginatedAsync(request.Page, request.PageSize);

			return jobs;
		}

		#endregion

	}
}
