namespace JobBoard.Core.Common.DTOs
{
	public class JobSummaryDto
	{
		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string CompanyName { get; set; }
		public required string Location { get; set; }
		public string? JobType { get; set; }
		public double MinSalary { get; set; }
		public double MaxSalary { get; set; }
		public string? ExperienceLevel { get; set; }
		public required DateTime DatePosted { get; set; }
		public required string Status { get; set; }

		public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
		public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

	}
}
