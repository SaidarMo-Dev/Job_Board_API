namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Responses
{
	public class GetUserDashboardStatsQueryResponse
	{
		public int TotalSavedJobs { get; set; }
		public int TotalApplications { get; set; }
		public int Rejected { get; set; }
		public int Pending { get; set; }
		public int ProfileCompletion { get; set; }
	}
}
