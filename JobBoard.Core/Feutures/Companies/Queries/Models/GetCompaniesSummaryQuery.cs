using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetCompaniesSummaryQuery : IRequest<PaginatedResponse<List<GetCompaniesSummaryQueryResponse>>>
	{
		public int page { get; set; }
		public int size { get; set; }
	}
}
