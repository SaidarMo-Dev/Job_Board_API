using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class UserSkill
	{
		public int Id { get; set; }

		public int UserId { get; set; }
		public int SkillId { get; set; }

		public string Level { get; set; } = null!;
		public int YearsOfExperience { get; set; }
		public bool IsPrimary { get; set; }

		public DateTime CreatedAt { get; set; }

		// Navigation
		public User User { get; set; } = null!;
		public Skill Skill { get; set; } = null!;
	}
}
