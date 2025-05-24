namespace JobBoard.Core.Feutures.Applications.Queries.Responses
{
	public class GetApplicationByJobIdQueryResponse
	{
		public List<ApplicationResponse> Applications { get; set; } = new();

	}

	public class ApplicationResponse
	{
		public int ApplicationId { get; set; }
		public int JobListingId { get; set; }
		public int UserId { get; set; }
		public string? Description { get; set; }
		public required DateTime CreatedOn { get; set; }
		public required string Status { get; set; }
		public required DateTime LastStatusDate { get; set; }

	}
}
