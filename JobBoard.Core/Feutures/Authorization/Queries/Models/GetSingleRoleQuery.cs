using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authorization.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Queries.Models
{
	public class GetSingleRoleQuery : IRequest<Response<GetSingleRoleQueryResponse>>
	{
		public int Id { get; set; }
	}
}
