using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authorization.Queries.Models;
using JobBoard.Core.Feutures.Authorization.Queries.Responses;
using JobBoard.Service.Authorization;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Queries.Handler
{
	public class AuthorizationQueryHandler : ResponseHandler,
				IRequestHandler<GetListRolesQuery, Response<List<GetListRolesQueryRsponse>>>,
				IRequestHandler<GetSingleRoleQuery, Response<GetSingleRoleQueryResponse>>


	{
		#region Fields
		private readonly IAuthorizationService _authorizationService;
		private readonly IMapper _mapper;

		#endregion


		#region Constructors
		public AuthorizationQueryHandler(IAuthorizationService authorizationService, IMapper mapper)
		{
			_authorizationService = authorizationService;
			_mapper = mapper;
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


		#endregion
	}
}
