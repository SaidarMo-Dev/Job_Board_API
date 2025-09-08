using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.commands.Models
{
	public class ResendVerificationCodeCommand : IRequest<Response<string>>
	{
		public required string Email { get; set; }
	}
}
