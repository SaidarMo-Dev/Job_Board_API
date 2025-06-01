using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Countries.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Countries.Queries.Models
{
	public class GetCountryByIdQuery : IRequest<Response<GetCountryByIdQueryResponse>>
	{
		public int Id { get; set; }
		public GetCountryByIdQuery(int id)
		{
			Id = id;
		}
	}
}
