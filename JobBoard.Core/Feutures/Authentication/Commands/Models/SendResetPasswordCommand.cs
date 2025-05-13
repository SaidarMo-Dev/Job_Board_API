using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class SendResetPasswordCommand : IRequest<Response<string>>
	{
		public required string Email { get; set; }
	}
}
