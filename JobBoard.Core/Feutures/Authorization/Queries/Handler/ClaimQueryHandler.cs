using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authorization.Queries.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Responses;
using JobBoard.Service.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Authorization.Queries.Handler
{
	public class ClaimQueryHandler : ResponseHandler,
			IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IAuthorizationService _authorizationService;
		private readonly UserManager<User> _userManager;

		#endregion

		#region Constructors
		public ClaimQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
									IAuthorizationService authorizationService,
									UserManager<User> userManager) : base(stringLocalizer)
		{
			_localizer = stringLocalizer;
			_authorizationService = authorizationService;
			_userManager = userManager;
		}

		#endregion

		#region Methods

		public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null) return NotFound<ManageUserClaimsResponse>();


			return Success(await _authorizationService.ManageUserClaimsAsync(user));

		}
		#endregion
	}
}
