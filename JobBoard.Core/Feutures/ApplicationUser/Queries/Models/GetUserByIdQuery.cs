
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Models
{
	public class GetUserByIdQuery(int id) : IRequest<Response<GetUserByIdQueryResponse>>
	{
		public int Id { get; set; } = id;
	}
}
