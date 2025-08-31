using JobBoard.Data.enums;

namespace JobBoard.Data.Responses
{
	public class RecentApplicationsResponse
	{

		public int Id { get; set; }
		public required string Position { get; set; }
		public required string Company { get; set; }
		public DateTime ApplicantDate { get; set; }
		public ApplicationStatusEnum Status { get; set; }
	}
}
