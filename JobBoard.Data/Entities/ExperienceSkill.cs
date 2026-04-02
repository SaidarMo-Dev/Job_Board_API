namespace JobBoard.Data.Entities
{
	public class ExperienceSkill
	{
		public int Id { get; set; }

		public int ExperienceId { get; set; }
		public int SkillId { get; set; }

		// Navigation
		public UserExperience Experience { get; set; } = null!;
		public Skill Skill { get; set; } = null!;
	}
}
