using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Queries.Models
{
	public class SendConfirmEmailQuery(int userId) : IRequest<Response<string>>
	{
		public int UserId { get; set; } = userId;
	}
}
