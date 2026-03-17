namespace JobBoard.Core.Feutures.Jobs.Queries.Responses
{
	public class GetJobByIdSummaryQueryResponse
	{
		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required int CompanyId { get; set; }
		public required string Location { get; set; }
		public required string JobType { get; set; }
		public string? MaxSalary { get; set; }
		public string? MinSalary { get; set; }
		public string? ExperienceLevel { get; set; }
		public required string DatePosted { get; set; }
		public required DateTime DateExpired { get; set; }
		public required string Status { get; set; }
		public required string CretaedByUser { get; set; }

		public int[]? SkillIds { get; set; }
		public int[]? CategoryIds { get; set; }

	}
}
