using JobBoard.Core.Feutures.Admin.Query.Responses;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Admin.Query.Models
{
	public class GetAdminJobsQuery : IRequest<PaginatedResponse<List<GetAdminJobsQueryResponse>>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
		public JobStatusEnum[]? JobStatus { get; set; }
		public string[]? Locations { get; set; }
		public string[]? Categories { get; set; }
		public string[]? Companies { get; set; }
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }

	}
}
