namespace JobBoard.Data.Entities
{
	public class JobSkill
	{
		public int JobListingId { get; set; }
		public int SkillId { get; set; }

		public JobListing jobListing { get; set; }
		public Skill skillInfo { get; set; }

	}


}

