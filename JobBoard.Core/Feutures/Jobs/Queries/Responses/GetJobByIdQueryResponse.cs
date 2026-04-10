using JobBoard.Core.Common.DTOs;

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
		public string? MaxSalary { get; set; }
		public string? MinSalary { get; set; }
		public string? ExperienceLevel { get; set; }
		public required string DatePosted { get; set; }
		public DateTime DateExpired { get; set; }
		public required string Status { get; set; }
		public required string CretaedByUser { get; set; }

		public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
		public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

	}
}
