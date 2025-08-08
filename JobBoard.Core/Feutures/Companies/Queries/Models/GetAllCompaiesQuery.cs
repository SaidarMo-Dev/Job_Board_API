using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetAllCompaiesQuery : IRequest<PaginatedResponse<List<GetListCompaniesQueryesponse>>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
		public SortCompany Sort { get; set; }
	}
}
