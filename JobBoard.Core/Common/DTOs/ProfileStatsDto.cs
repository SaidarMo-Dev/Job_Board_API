namespace JobBoard.Core.Common.DTOs
{
	public class ProfileStatsDto
	{
		public int ProfileCompletion { get; set; }

		public int TotalSkills { get; set; }
		public int TotalExperiences { get; set; }
		public int TotalEducations { get; set; }
		public int TotalCertifications { get; set; }
		public int TotalLanguages { get; set; }

		public int ProfileViews { get; set; }

		public DateTime LastUpdated { get; set; }
	}
}
