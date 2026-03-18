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
		public string? Name { get; set; }
		public string? Location { get; set; }
		public SortCompany Sort { get; set; }
	}
}
