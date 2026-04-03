namespace JobBoard.Core.Common.DTOs
{
	public class UserSkillDto
	{
		public int SkillId { get; set; }
		public string Name { get; set; } = null!;
		public string Level { get; set; } = null!;
		public int YearsOfExperience { get; set; }
		public bool IsPrimary { get; set; }
	}
}
