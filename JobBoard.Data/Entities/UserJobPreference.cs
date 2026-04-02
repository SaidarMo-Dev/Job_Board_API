using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class UserJobPreference
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string? DesiredJobTitle { get; set; }
		public decimal? DesiredSalaryMin { get; set; }
		public decimal? DesiredSalaryMax { get; set; }

		public string? PreferredLocation { get; set; }
		public string? WorkType { get; set; }

		public bool IsOpenToWork { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public User User { get; set; } = null!;
	}
}
