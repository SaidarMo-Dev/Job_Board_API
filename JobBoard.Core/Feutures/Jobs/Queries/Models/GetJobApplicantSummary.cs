using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetJobApplicantSummary : IRequest<PaginatedResponse<List<GetJobApplicantSummaryResponse>>>
	{
		public int JobId { get; set; }
		public int Page { get; set; }
		public int Size { get; set; }

	}
}
