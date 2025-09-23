using JobBoard.Core.Feutures.Categories.Queries.Results;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Categories.Queries.Models
{
	public class GetCategoriesSummaryQuery : IRequest<PaginatedResponse<List<GetCategoriesSummaryQueryResponse>>>
	{
		public int page { get; set; }
		public int size { get; set; }
	}
}
