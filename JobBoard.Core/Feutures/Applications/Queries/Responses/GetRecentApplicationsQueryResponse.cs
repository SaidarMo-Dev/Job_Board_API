namespace JobBoard.Core.Feutures.Applications.Queries.Responses
{
	public class GetRecentApplicationsQueryResponse
	{
		public int Id { get; set; }
		public required string Position { get; set; }
		public required string Company { get; set; }
		public DateTime ApplicantDate { get; set; }
		public required string Status { get; set; }


	}
}
