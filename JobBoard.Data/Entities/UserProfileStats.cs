using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class UserProfileStats
	{
		public int UserId { get; set; }

		public int ProfileCompletion { get; set; }
		public int TotalSkills { get; set; }
		public int TotalExperiences { get; set; }
		public int TotalEducations { get; set; }
		public int TotalCertifications { get; set; }
		public int TotalLanguages { get; set; }

		public int ProfileViews { get; set; }
		public DateTime LastUpdated { get; set; }

		public User User { get; set; } = null!;
	}
}
