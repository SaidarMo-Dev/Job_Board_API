using JobBoard.Data.enums;

namespace JobBoard.Core.Feutures.Jobs.Queries.Responses
{
	public class GetJobApplicantSummaryResponse
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public string? ImagePath { get; set; }
		public string? Email { get; set; }
		public required string Experience { get; set; }
		public string? Country { get; set; }
		public DateTime AppliedDate { get; set; }
		public ApplicationStatusEnum Status { get; set; }
		public string? Resume { get; set; }



	}
}
