using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetFeaturedCompaniesQuery : IRequest<PaginatedResponse<GetSingleCompanyQueryResponse>>
	{
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
