namespace JobBoard.Data.Responses
{
	public class RecentSavedJobsResponse
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required string Company { get; set; }
		public DateTime SavedAt { get; set; }
	}
}
