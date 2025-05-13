using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class ResetPasswordCommand : IRequest<Response<string>>
	{
		public required string Email { get; set; }
	}
}
