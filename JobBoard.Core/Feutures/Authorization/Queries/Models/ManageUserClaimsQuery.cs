using JobBoard.Core.Bases;
using JobBoard.Data.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Queries.Models
{
	public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResponse>>
	{
		public int UserId { get; set; }
	}
}
