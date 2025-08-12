using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using JobBoard.Infrastructure.context;

namespace JobBoard.Core.Seeders
{
	public static class JobSeeder
	{
		private static List<JobListing> GetJobs()
		{
			return new List<JobListing>
			{
				new JobListing { Title = "Mobile Developer", Description = "Build native Android and iOS applications with cross-platform tools.", CompanyId = 3, Location = "Miami, FL", JobType = JobTypeEnum.FullTime, MinSalary = 80000, MaxSalary = 110000, ExperienceLevel = ExperienceLevelEnum.MidLevel, DatePosted = DateTime.UtcNow.AddDays(-7), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "QA Engineer", Description = "Develop test plans, write scripts, and ensure product quality.", CompanyId = 1, Location = "Denver, CO", JobType = JobTypeEnum.PartTime, MinSalary = 50000, MaxSalary = 75000, ExperienceLevel = ExperienceLevelEnum.EntryLevel, DatePosted = DateTime.UtcNow.AddDays(-3), Status = JobStatusEnum.Pending, CreatedByUserId = 1 },
				new JobListing { Title = "DevOps Engineer", Description = "Maintain CI/CD pipelines and automate infrastructure deployment.", CompanyId = 2, Location = "Seattle, WA", JobType = JobTypeEnum.FullTime, MinSalary = 85000, MaxSalary = 120000, ExperienceLevel = ExperienceLevelEnum.SeniorLevel, DatePosted = DateTime.UtcNow.AddDays(-18), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "Project Manager", Description = "Oversee project execution, timelines, deliverables, and stakeholder communication.", CompanyId = 3, Location = "Boston, MA", JobType = JobTypeEnum.Contract, MinSalary = 95000, MaxSalary = 125000, ExperienceLevel = ExperienceLevelEnum.LeadPrincipal, DatePosted = DateTime.UtcNow.AddDays(-22), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "IT Support Specialist", Description = "Provide technical support and troubleshooting for hardware and software.", CompanyId = 1, Location = "Phoenix, AZ", JobType = JobTypeEnum.PartTime, MinSalary = 45000, MaxSalary = 60000, ExperienceLevel = ExperienceLevelEnum.EntryLevel, DatePosted = DateTime.UtcNow.AddDays(-2), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "Cloud Architect", Description = "Design scalable, secure, and cost-effective cloud-based architectures.", CompanyId = 2, Location = "Dallas, TX", JobType = JobTypeEnum.FullTime, MinSalary = 120000, MaxSalary = 150000, ExperienceLevel = ExperienceLevelEnum.LeadPrincipal, DatePosted = DateTime.UtcNow.AddDays(-8), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "System Administrator", Description = "Manage servers, network configurations, and user access policies.", CompanyId = 3, Location = "Atlanta, GA", JobType = JobTypeEnum.FullTime, MinSalary = 70000, MaxSalary = 95000, ExperienceLevel = ExperienceLevelEnum.MidLevel, DatePosted = DateTime.UtcNow.AddDays(-4), Status = JobStatusEnum.Pending, CreatedByUserId = 1 },
				new JobListing { Title = "Content Writer", Description = "Produce SEO-optimized content for blogs, websites, and documentation.", CompanyId = 1, Location = "Remote", JobType = JobTypeEnum.FreeLance, MinSalary = 30000, MaxSalary = 50000, ExperienceLevel = ExperienceLevelEnum.EntryLevel, DatePosted = DateTime.UtcNow.AddDays(-1), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "Cybersecurity Analyst", Description = "Monitor and secure systems against data breaches and cyberattacks.", CompanyId = 2, Location = "Washington, DC", JobType = JobTypeEnum.FullTime, MinSalary = 95000, MaxSalary = 130000, ExperienceLevel = ExperienceLevelEnum.MidLevel, DatePosted = DateTime.UtcNow.AddDays(-11), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "AI Engineer", Description = "Design, train, and deploy AI models for enterprise applications.", CompanyId = 3, Location = "San Jose, CA", JobType = JobTypeEnum.FullTime, MinSalary = 110000, MaxSalary = 140000, ExperienceLevel = ExperienceLevelEnum.SeniorLevel, DatePosted = DateTime.UtcNow.AddDays(-6), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "Business Analyst", Description = "Gather requirements and improve processes using data-driven insights.", CompanyId = 1, Location = "Houston, TX", JobType = JobTypeEnum.Contract, MinSalary = 60000, MaxSalary = 85000, ExperienceLevel = ExperienceLevelEnum.MidLevel, DatePosted = DateTime.UtcNow.AddDays(-9), Status = JobStatusEnum.Pending, CreatedByUserId = 1 },
				new JobListing { Title = "Recruiter", Description = "Source, interview, and manage the hiring process for roles.", CompanyId = 2, Location = "Philadelphia, PA", JobType = JobTypeEnum.PartTime, MinSalary = 45000, MaxSalary = 65000, ExperienceLevel = ExperienceLevelEnum.EntryLevel, DatePosted = DateTime.UtcNow.AddDays(-13), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "Machine Learning Engineer", Description = "Develop machine learning models for real-time analytics systems.", CompanyId = 3, Location = "Palo Alto, CA", JobType = JobTypeEnum.FullTime, MinSalary = 115000, MaxSalary = 145000, ExperienceLevel = ExperienceLevelEnum.LeadPrincipal, DatePosted = DateTime.UtcNow.AddDays(-16), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "Database Administrator", Description = "Manage database performance, backup, security, and disaster recovery.", CompanyId = 1, Location = "Orlando, FL", JobType = JobTypeEnum.FullTime, MinSalary = 75000, MaxSalary = 100000, ExperienceLevel = ExperienceLevelEnum.MidLevel, DatePosted = DateTime.UtcNow.AddDays(-17), Status = JobStatusEnum.Active, CreatedByUserId = 1 },
				new JobListing { Title = "Product Manager", Description = "Lead cross-functional teams to deliver user-centric digital products.", CompanyId = 2, Location = "Portland, OR", JobType = JobTypeEnum.FullTime, MinSalary = 100000, MaxSalary = 135000, ExperienceLevel = ExperienceLevelEnum.SeniorLevel, DatePosted = DateTime.UtcNow.AddDays(-14), Status = JobStatusEnum.Pending, CreatedByUserId = 1 },

			};

		}
		public static async Task SeedAsync(appDbContext dbContext)
		{
			if (dbContext.jobs.Count() == 0)
			{
				dbContext.jobs.AddRange(GetJobs());
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
