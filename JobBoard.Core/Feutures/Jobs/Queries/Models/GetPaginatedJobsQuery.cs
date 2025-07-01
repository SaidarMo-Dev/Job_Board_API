using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetPaginatedJobsQuery : IRequest<PaginatedResponse<List<GetPaginatedJobsQueryResponse>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? SearchByTitle { get; set; }
		public string? SearchByLocation { get; set; }
		public JobTypeEnum? JobType { get; set; }
		public double? SalaryMin { get; set; }
		public double? SalaryMax { get; set; }
		public ExperienceLevelEnum? ExperienceLevel { get; set; }
		public SortEnum? SortBy { get; set; }



	}
}
