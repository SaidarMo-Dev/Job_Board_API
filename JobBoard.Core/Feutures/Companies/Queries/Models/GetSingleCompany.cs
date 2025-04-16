using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Companies.Queries.Results;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetSingleCompanyQuery : IRequest<Response<GetSingleCompanyQueryResponse>>
	{
		public int Id { get; set; }
		public GetSingleCompanyQuery(int id)
		{
			Id = id;
		}
	}
}
