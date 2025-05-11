using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authentication.Queries.Models;
using JobBoard.Core.Resources;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Authentication.Queries
{
	public class AuthenticationQueryHandler : ResponseHandler,
					IRequestHandler<ConfirmEmailQuery, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IUserService _userService;

		#endregion

		#region Constructors
		public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
										 IUserService userService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_userService = userService;

		}


		#endregion

		#region Handle Methods
		public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
		{
			var result = await _userService.ConfirmEmailAsync(request.UserId, request.Code);

			if (!(result == "Success")) return BadRequest<string>(result);

			return Success<string>(message: "Email Confirmed");
		}
		#endregion

	}
}
