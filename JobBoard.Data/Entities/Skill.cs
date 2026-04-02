namespace JobBoard.Data.Entities
{
	public class Skill
	{
		public int SkillId { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
		public string NormalizedName { get; set; } = null!;
		public string? Category { get; set; }
		public bool IsApproved { get; set; }
		public DateOnly? CreateAt { get; set; }


		// Navigation
		public ICollection<JobSkill> jobSkills { get; set; } = new List<JobSkill>();

		public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
		public ICollection<ExperienceSkill> ExperienceSkills { get; set; } = new List<ExperienceSkill>();
	}
}

