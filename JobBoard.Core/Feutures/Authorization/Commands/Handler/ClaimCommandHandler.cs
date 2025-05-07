using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authorization.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Service.Authorization;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Authorization.Commands.Handler
{
	public class ClaimCommandHandler : ResponseHandler,
				IRequestHandler<UpdateUserClaimCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;

		#endregion

		#region Constructors
		public ClaimCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
									IAuthorizationService authorizationService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
		}
		#endregion

		#region Handle Methods
		public async Task<Response<string>> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.UpdateUserClaimsAsnyc(request);

			switch (result)
			{
				case "NotFound":
					return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

				case "FailedToDeleteUserClaims":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToDeleteUserClaims]);

				case "FailedToAddClaims":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddClaims]);

				case "Success":
					return Success<string>();

				case "ErrorUpdateClaims":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorUpdateClaims]);

				default:
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorUpdateClaims]);

			}
		}

		#endregion

	}
}
