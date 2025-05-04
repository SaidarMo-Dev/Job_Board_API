using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authorization.Queries.Models;
using JobBoard.Core.Feutures.Authorization.Queries.Responses;
using JobBoard.Data.DTOs;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Core.Feutures.Authorization.Queries.Handler
{
	public class AuthorizationQueryHandler : ResponseHandler,
				IRequestHandler<GetListRolesQuery, Response<List<GetListRolesQueryRsponse>>>,
				IRequestHandler<GetSingleRoleQuery, Response<GetSingleRoleQueryResponse>>,
				IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesDto>>


	{
		#region Fields
		private readonly IAuthorizationService _authorizationService;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		#endregion


		#region Constructors
		public AuthorizationQueryHandler(IAuthorizationService authorizationService, IMapper mapper,
										UserManager<User> userManager)
		{
			_authorizationService = authorizationService;
			_mapper = mapper;
			_userManager = userManager;
		}

		#endregion

		#region Methods
		public async Task<Response<List<GetListRolesQueryRsponse>>> Handle(GetListRolesQuery request, CancellationToken cancellationToken)
		{
			var roles = await _authorizationService.GetListRolesAsync();

			return Success(_mapper.Map<List<GetListRolesQueryRsponse>>(roles));
		}

		public async Task<Response<GetSingleRoleQueryResponse>> Handle(GetSingleRoleQuery request, CancellationToken cancellationToken)
		{
			var role = await _authorizationService.GetRoleByIdAsync(request.Id);

			if (role is null) return NotFound<GetSingleRoleQueryResponse>();

			return Success(_mapper.Map<GetSingleRoleQueryResponse>(role));
		}

		public async Task<Response<ManageUserRolesDto>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user is null) return NotFound<ManageUserRolesDto>("User Not Found");

			var result = await _authorizationService.GetManageUserRolesAsync(user);

			return Success(result);

		}

		#endregion
	}
}
