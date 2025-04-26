namespace JobBoard.Core.Feutures.Jobs.Queries.Responses
{
	public class GetJobByIdQueryResponse
	{
		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string CompanyName { get; set; }
		public required string Location { get; set; }
		public required string JobType { get; set; }
		public string? SalaryRange { get; set; }
		public required string DatePosted { get; set; }
		public required string status { get; set; }
		public required string CretaedByUser { get; set; }

	}
}
