using JobBoard.Core.Bases;
using JobBoard.Data.Helpers;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class SignInCommand : IRequest<Response<AuthResponse>>
	{
		public required string Username { get; set; }
		public required string Password { get; set; }

	}
}
