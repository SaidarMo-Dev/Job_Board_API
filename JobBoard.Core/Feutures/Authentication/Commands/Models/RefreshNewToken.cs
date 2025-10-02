using JobBoard.Core.Bases;
using JobBoard.Data.Helpers;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class RefreshNewAccessToken : IRequest<Response<AuthResponse>>
	{
		public required string RefreshToken { get; set; }

	}
}
