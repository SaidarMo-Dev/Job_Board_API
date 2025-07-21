using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authentication.Queries.Models;
using JobBoard.Core.Resources;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Authentication.Queries.Handlers
{
	public class AuthenticationQueryHandler : ResponseHandler,
					IRequestHandler<ConfirmEmailQuery, Response<string>>,
					IRequestHandler<ConfirmResetPasswordQuery, Response<string>>,
					IRequestHandler<SendConfirmEmailQuery, Response<string>>,
					IRequestHandler<ConfirmEmailByCode, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IUserService _userService;
		private readonly IAuthenticationService _authenticationService;

		#endregion

		#region Constructors
		public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
										 IUserService userService,
										  IAuthenticationService authenticationService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_userService = userService;
			_authenticationService = authenticationService;
		}


		#endregion

		#region Handle Methods
		public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.ConfirmEmailByUrlAsync(request.UserId, request.Code);

			if (!(result == "Success")) return BadRequest<string>(result);

			return Success<string>(message: "Email Confirmed");
		}

		public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.ConfirmResetPasswordAsync(request.Email, request.Code);

			switch (result)
			{
				case "UserNotFound": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
				case "IncorrectCode": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.IncorrectCode]);
				case "Success": return Success<string>("", _stringLocalizer[SharedResourcesKeys.Success]);

				default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Failed]);
			}
		}

		public async Task<Response<string>> Handle(SendConfirmEmailQuery request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.SendUrlConfirmEmailAsync(request.UserId);

			if (result == "UserNotFound") return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
			else if (result == "Success") return Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailSend]);


			return BadRequest(result);


		}

		public async Task<Response<string>> Handle(ConfirmEmailByCode request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.ConfirmEmailByCodeAsync(request.Email, request.Code);

			if (!(result == "Success")) return BadRequest<string>("Inccorect code!");

			return Success<string>(message: "Email Confirmed");
		}


		#endregion

	}
}
