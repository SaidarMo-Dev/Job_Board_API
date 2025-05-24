namespace JobBoard.Core.Feutures.Jobs.Queries.Responses
{
	public class GetJobsByCompanyIdQueryResponse
	{
		public int CompanyId { get; set; }
		public List<JobResponse> Jobs { get; set; } = new();

	}

	public class JobResponse
	{
		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string Location { get; set; }
		public required string JobType { get; set; }
		public string? SalaryRange { get; set; }
		public required DateTime DatePosted { get; set; }
		public required string Status { get; set; }

	}
}
