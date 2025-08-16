using JobBoard.Core.Common.DTOs;

namespace JobBoard.Core.Feutures.Admin.Query.Responses
{
	public class GetAdminJobsQueryResponse
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required CompanyDto Company { get; set; }
		public required string Location { get; set; }
		public string? JobType { get; set; }
		public double MinSalary { get; set; }
		public double MaxSalary { get; set; }
		public string? ExperienceLevel { get; set; }
		public DateTime DatePosted { get; set; }
		public DateTime? DateExpired { get; set; }
		public string? Status { get; set; }
		public int ApplicantsCount { get; set; }
		public SkillDto[]? Skills { get; set; }
		public CategoryDto[]? Categories { get; set; }
		public required string createdBy { get; set; }
	}

}
