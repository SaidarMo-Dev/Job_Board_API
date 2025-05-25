namespace JobBoard.Core.Common.DTOs
{
	public class JobSummaryDto
	{
		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string CompanyId { get; set; }
		public required string Location { get; set; }
		public required string JobType { get; set; }
		public string? SalaryRange { get; set; }
		public required DateTime DatePosted { get; set; }
		public required string Status { get; set; }

	}
}
