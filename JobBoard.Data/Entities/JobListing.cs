using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;

namespace JobBoard.Data.Entities
{
	public class JobListing
	{

		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public int CompanyId { get; set; }
		public required string Location { get; set; }
		public JobTypeEnum JobType { get; set; }
		public double MinSalary { get; set; }
		public double MaxSalary { get; set; }
		public ExperienceLevelEnum ExperienceLevel { get; set; }
		public required DateTime DatePosted { get; set; }
		public JobStatusEnum Status { get; set; }
		public int CreatedByUserId { get; set; }

		public ICollection<JobSkill> JobSkills { get; set; }
		public Company company { get; set; }
		public ICollection<Bookmark> bookMarks { get; set; }
		public ICollection<Application> applications { get; set; }
		public ICollection<JobCategory> jobCategories { get; set; } = new List<JobCategory>();
		public User UserInfo { get; set; }


	}
}




