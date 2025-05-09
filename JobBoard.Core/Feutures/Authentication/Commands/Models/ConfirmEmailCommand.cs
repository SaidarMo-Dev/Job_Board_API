using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class ConfirmEmailCommand : IRequest<Response<string>>
	{
		public int UserId { get; set; }
		public string Code { get; set; }

	}
}
