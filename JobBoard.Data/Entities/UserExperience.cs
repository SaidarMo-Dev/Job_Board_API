using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class UserExperience
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string JobTitle { get; set; } = null!;
		public string CompanyName { get; set; } = null!;
		public string? CompanyLogoUrl { get; set; }

		public string EmploymentType { get; set; } = null!;
		public string? Location { get; set; }
		public bool IsRemote { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool IsCurrent { get; set; }

		public string? Description { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation
		public User User { get; set; } = null!;
		public ICollection<ExperienceSkill> Skills { get; set; } = new List<ExperienceSkill>();
	}
}
