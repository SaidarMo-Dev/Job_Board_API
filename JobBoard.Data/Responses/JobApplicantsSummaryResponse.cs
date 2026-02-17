using JobBoard.Data.enums;

namespace JobBoard.Data.Responses
{
	public class JobApplicantsSummaryResponse
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public int? ProfileImageFileId { get; set; }
		public string? Email { get; set; }
		public required string Experience { get; set; }
		public DateTime AppliedDate { get; set; }
		public ApplicationStatusEnum Status { get; set; }
		public int? ResumeFileId { get; set; }

	}
}
