using JobBoard.Core.Common.DTOs;

namespace JobBoard.Core.Feutures.Applications.Queries.Responses
{
	public class GetCurrentUserApplicationsQueryResponse
	{
		public int ApplicationId { get; set; }
		public required JobSummaryDto Job { get; set; }
		public required string Status { get; set; }
		public required DateTime LastStatusDate { get; set; }
	}
}
