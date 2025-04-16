namespace JobBoard.Data.Entities
{
	public class Skill
	{
		public int SkillId { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }

		public ICollection<JobSkill> jobSkills { get; set; }
	}
}

