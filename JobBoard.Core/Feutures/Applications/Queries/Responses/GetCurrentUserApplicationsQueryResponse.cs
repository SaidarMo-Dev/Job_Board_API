using JobBoard.Core.Common.DTOs;

namespace JobBoard.Core.Feutures.Applications.Queries.Responses
{
	public class GetCurrentUserApplicationsQueryResponse
	{
		public List<UserApplicationResponse> applications { get; set; } = new();
	}

	public class UserApplicationResponse
	{
		public int ApplicationId { get; set; }
		public required JobSummaryDto Job { get; set; }
		public string? Description { get; set; }
		public required DateTime CreatedOn { get; set; }
		public required string Status { get; set; }
		public required DateTime LastStatusDate { get; set; }
	}
}
