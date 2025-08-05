using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Admin.Query.Models;
using JobBoard.Core.Feutures.Admin.Query.Responses;
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
		IRequestHandler<GetUsersQuery, PaginatedResponse<List<UserManagementResponse>>>,
		IRequestHandler<GetAdminProfileQuery, Response<GetAdminProfileQueryResponse>>
	{
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ICurrentUserService _currentUserService;
		#region Fields

		#endregion

		#region Constructors
		public AdminQueryHandler(IUserService userService,
								UserManager<User> userManager,
								IMapper mapper,
								IStringLocalizer<SharedResources> stringLocalizer,
								ICurrentUserService currentUserService) : base(stringLocalizer)
		{
			_userService = userService;
			_userManager = userManager;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
			_currentUserService = currentUserService;
		}
		#endregion

		#region Handles
		public async Task<PaginatedResponse<List<UserManagementResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
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

		#endregion

	}
}
