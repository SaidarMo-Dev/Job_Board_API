namespace JobBoard.Data.Responses
{
	public class DashboardStatsResponse
	{
		public int TotalSavedJobs { get; set; }
		public int TotalApplications { get; set; }
		public int Rejected { get; set; }
		public int Pending { get; set; }
	}
}
