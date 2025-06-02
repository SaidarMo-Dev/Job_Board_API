
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Countries.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Countries.Queries.Models
{
	public class GetListCountriesQuery : IRequest<Response<List<ListCountriesQueryResponse>>>
	{

	}
}
