using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authorization.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Queries.Models
{
	public class GetListRolesQuery : IRequest<Response<List<GetListRolesQueryRsponse>>>
	{
	}
}
