using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class ConfirmEmailByCode : IRequest<Response<string>>
	{
		public string Email { get; set; }
		public string Code { get; set; }
	}
}
