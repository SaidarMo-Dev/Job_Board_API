using JobBoard.Core.Common.DTOs;

namespace JobBoard.Core.Feutures.Applications.Queries.Responses
{
	public class GetApplicationsByJobIdQueryResponse
	{
		public List<ApplicationResponse> Applications { get; set; } = new();

	}

	public class ApplicationResponse
	{
		public int ApplicationId { get; set; }
		public required JobSummaryDto Job { get; set; }
		public required UserSummaryDto User { get; set; }
		public string? Description { get; set; }
		public required DateTime CreatedOn { get; set; }
		public required string Status { get; set; }
		public required DateTime LastStatusDate { get; set; }

	}
}
