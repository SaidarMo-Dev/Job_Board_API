using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetCompaiesQuery : IRequest<PaginatedResponse<GetListCompaniesQueryesponse>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }

		public string? Search { get; set; }

		public CompanySize[]? Size { get; set; }
		public string[]? Industries { get; set; }

		public CompanySortBy SortBy { get; set; } = CompanySortBy.Name;
		public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
	}
}
