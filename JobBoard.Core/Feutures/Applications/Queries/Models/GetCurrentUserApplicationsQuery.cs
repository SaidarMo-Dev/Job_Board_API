using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Queries.Models
{
	public class GetCurrentUserApplicationsQuery : IRequest<PaginatedResponse<List<GetCurrentUserApplicationsQueryResponse>>>
	{
		public int Page { get; set; }
		public int Size { get; set; }
		public ApplicationStatusFilter StatusFilter { get; set; }
	}
}
