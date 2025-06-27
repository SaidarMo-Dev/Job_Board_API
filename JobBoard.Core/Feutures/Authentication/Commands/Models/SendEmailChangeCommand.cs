using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class SendEmailChangeCommand : IRequest<Response<string>>
	{
		public string CurrentEmail { get; set; }
		public string NewEmail { get; set; }
	}
}
