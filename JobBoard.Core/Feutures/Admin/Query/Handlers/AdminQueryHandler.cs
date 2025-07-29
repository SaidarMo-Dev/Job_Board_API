using AutoMapper;
using JobBoard.Core.Feutures.Admin.Query.Models;
using JobBoard.Core.Wrapers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Responses;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Core.Feutures.Admin.Query.Handlers
{
	public class AdminQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResponse<List<UserManagementResponse>>>
	{
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		#region Fields

		#endregion

		#region Constructors
		public AdminQueryHandler(IUserService userService, UserManager<User> userManager,
			IMapper mapper)
		{
			_userService = userService;
			_userManager = userManager;
			_mapper = mapper;
		}
		#endregion

		#region Handles
		public async Task<PaginatedResponse<List<UserManagementResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var users = _userService.GetUsersQueryable(request.Search, request.FilterByRole, request.FilterStatus);

			var usersDto = await users.ToPaginatedAsync(request.Page, request.Size);

			return usersDto;

		}

		#endregion

	}
}
