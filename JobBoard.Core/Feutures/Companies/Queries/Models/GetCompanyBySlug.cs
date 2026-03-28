using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Companies.Queries.Results;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetCompanyBySlug : IRequest<Response<GetCompanyBySlugQueryResponse>>
	{
		public required string slug { get; set; }
	}
}
