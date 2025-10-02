namespace JobBoard.Core.Feutures.Employer.Queries.Responses
{
	public class GetEmployerDashboardStatsQueryResponse
	{
		public int TotalJobs { get; set; }
		public int ActiveJobs { get; set; }
		public int ApplicationsReceived { get; set; }

	}
}
