using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class VerifyEmailChangeCommand : IRequest<Response<string>>
	{
		public string OldEmail { get; set; }
		public string NewEmail { get; set; }
		public string Code { get; set; }
	}
}
