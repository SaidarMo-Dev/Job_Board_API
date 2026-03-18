using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetPaginatedJobsQuery : IRequest<PaginatedResponse<GetPaginatedJobsQueryResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? SearchByTitle { get; set; }
		public string? SearchByLocation { get; set; }

		public SortEnum? SortBy { get; set; }
		public string[]? JobTypes { get; set; }
		public string[]? ExperienceLevels { get; set; }
		public string[]? PopularCategories { get; set; }
		public string[]? PopularCompanies { get; set; }



	}
}
