using JobBoard.Core.Feutures.Categories.Queries.Results;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Categories.Queries.Models
{
	public class GetListCategoriesQuery : IRequest<PaginatedResponse<GetListCategoriesQueryResponse>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
		public SortCategory sort { get; set; }
	}
}
