using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Queries.Models
{
	public class ConfirmEmailQuery : IRequest<Response<string>>
	{
		public int UserId { get; set; }
		public string Code { get; set; }
	}

}
