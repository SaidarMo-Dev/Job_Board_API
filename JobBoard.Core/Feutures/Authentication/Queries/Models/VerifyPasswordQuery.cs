using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Queries.Models
{
	public class VerifyPasswordQuery : IRequest<Response<bool>>
	{
		public required string Password { get; set; }
	}
}
