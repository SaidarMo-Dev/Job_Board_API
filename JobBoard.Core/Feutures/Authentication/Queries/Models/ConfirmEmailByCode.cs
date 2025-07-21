using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Queries.Models
{
	public class ConfirmEmailByCode : IRequest<Response<string>>
	{
		public required string Email { get; set; }
		public required string Code { get; set; }
	}
}
