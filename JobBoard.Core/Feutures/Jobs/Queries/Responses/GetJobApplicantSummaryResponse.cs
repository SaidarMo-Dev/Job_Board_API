using JobBoard.Data.enums;

namespace JobBoard.Core.Feutures.Jobs.Queries.Responses
{
	public class GetJobApplicantSummaryResponse
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public int? ProfileImageFileId { get; set; }
		public string? ProfileImageUrl { get; set; }
		public string? Email { get; set; }
		public required string Experience { get; set; }
		public string? Country { get; set; }
		public DateTime AppliedDate { get; set; }
		public ApplicationStatusEnum Status { get; set; }
		public int? ResumeFileId { get; set; }


	}
}
