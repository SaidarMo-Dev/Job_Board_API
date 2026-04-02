using JobBoard.Core.Feutures.Jobs.Queries.Responses;

namespace JobBoard.Core.Common.DTOs
{
	public class GlobalJobResponseDto
	{
		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required CompanyPreviewDto Company { get; set; }
		public required string Location { get; set; }
		public required string JobType { get; set; }
		public string? MaxSalary { get; set; }
		public string? MinSalary { get; set; }
		public string? ExperienceLevel { get; set; }
		public required DateTime DatePosted { get; set; }
		public required string Status { get; set; }
		public required string CretaedByUser { get; set; }

		public List<SkillDto>? Skills { get; set; }
		public List<CategoryDto>? Categories { get; set; }
	}
}
