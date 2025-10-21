using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetJobApplicantsSummary : IRequest<PaginatedResponse<List<GetJobApplicantSummaryResponse>>>
	{
		public int JobId { get; set; }
		public int Page { get; set; }
		public int Size { get; set; }
		public SortApplicantsEnum Sort { get; set; }
		public FilterApplicantsEnum Filter { get; set; }

	}
}
