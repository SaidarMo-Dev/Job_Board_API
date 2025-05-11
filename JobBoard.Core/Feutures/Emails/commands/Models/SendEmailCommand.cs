using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Emails.commands.Models
{
	public class SendEmailCommand : IRequest<Response<string>>
	{
		public required string Email { get; set; }
		public required string Message { get; set; }
	}
}
