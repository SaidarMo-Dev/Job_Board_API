using JobBoard.Core.Common.DTOs;

namespace JobBoard.Core.Feutures.Employer.Queries.Responses
{
	public class GetEmployerPostedJobsQueryResponse
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string Company { get; set; }
		public List<CategoryDto> Categories { get; set; } = new();
		public List<SkillDto> Skills { get; set; } = new();
		public required string Location { get; set; }
		public required string JobType { get; set; }
		public required string ExperienceLevel { get; set; }
		public required string Status { get; set; }
		public DateTime PostedDate { get; set; }
		public DateTime ExpiryDate { get; set; }
		public int ApplicantsCount { get; set; }
		public double? MinSalary { get; set; }
		public double? MaxSalary { get; set; }
		public required string CreatedBy { get; set; }
	}
}
