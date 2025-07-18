using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Queries.Models
{
	public class GetCurrentUserApplicationsQuery(int page, int size) : IRequest<PaginatedResponse<List<GetCurrentUserApplicationsQueryResponse>>>
	{
		public int Page { get; set; } = page;
		public int Size { get; set; } = size;
	}
}
