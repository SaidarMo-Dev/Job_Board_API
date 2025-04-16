using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetPaginatedJobsQuery : IRequest<PaginatedResponse<List<GetPaginatedJobsQueryResponse>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
