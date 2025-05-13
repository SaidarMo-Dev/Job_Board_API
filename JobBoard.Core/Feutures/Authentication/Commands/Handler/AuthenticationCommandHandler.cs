using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authentication.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Authentication.Commands.Handler
{
	public class AuthenticationCommandHandler : ResponseHandler,
			IRequestHandler<SignInCommand, Response<AuthResponse>>,
			IRequestHandler<RefreshNewAccessToken, Response<AuthResponse>>,
			IRequestHandler<SendResetPasswordCommand, Response<string>>,
			IRequestHandler<ResetPasswordCommand, Response<string>>

	{
		#region Fields
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly IAuthenticationService _authenticationService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IUserService _userService;
		#endregion

		#region Constructors
		public AuthenticationCommandHandler(SignInManager<User> signInManager,
									UserManager<User> userManager,
									IAuthenticationService authenticationService,
									IStringLocalizer<SharedResources> stringLocalizer,
									IUserService userService) : base(stringLocalizer)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_authenticationService = authenticationService;
			_stringLocalizer = stringLocalizer;
			_userService = userService;
		}

		#endregion

		#region Handles
		public async Task<Response<AuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByNameAsync(request.Username);
			if (user == null) return NotFound<AuthResponse>("Incorect Username Or Password!");

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			if (!result.Succeeded) return NotFound<AuthResponse>($"Incorect Username Or Password!");

			// generate token

			var TokneResult = await _authenticationService.GenerateUserToken(user);

			return Success(TokneResult);
		}

		public async Task<Response<AuthResponse>> Handle(RefreshNewAccessToken request, CancellationToken cancellationToken)
		{

			var response = await _authenticationService.GetRefreshToken(request.RefreshToken, request.AccessToken);

			return Success(response);
		}

		public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.SendResetPasswordAsync(request.Email);

			switch (result)
			{
				case "UserNotFound": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
				case "ErrorUpdateUser": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorUpdateUser]);
				case "Success": return Success("", _stringLocalizer[SharedResourcesKeys.Success]);
				case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Failed]);

				default: return BadRequest<string>(result);
			}

		}

		public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.ResetPasswordAsync(request.Email, request.Password);

			var switchResult = result switch
			{
				"UserNotFound" => NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]),
				"FailedRemovePassword" or "FailedAddPassword" => BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedResetPassword]),
				"Success" => Success("", _stringLocalizer[SharedResourcesKeys.Success]),
				_ => BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Failed])
			};


			return switchResult;
		}



		#endregion
	}
}
