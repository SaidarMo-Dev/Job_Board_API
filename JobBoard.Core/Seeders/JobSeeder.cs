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
				new JobListing
				{

					Title = "Senior Backend Engineer (C#/.NET)",
					Description = "Join our core platform team to build high-throughput IoT data pipelines using .NET 8 and Azure.",
					CompanyId = 1,
					Location = "Remote / San Francisco",
					JobType = JobTypeEnum.FullTime,
					MinSalary = 140000,
					MaxSalary = 185000,
					ExperienceLevel = ExperienceLevelEnum.SeniorLevel,
					DatePosted = DateTime.UtcNow.AddDays(-2),
					DateExpired = DateTime.UtcNow.AddDays(28),
					Status = JobStatusEnum.Active,
					CreatedByUserId = 1,
					JobSkills = new List<JobSkill>(), // To be populated with "C#", "Cloud", "SQL"
					jobCategories = new List<JobCategory>()
				},
				new JobListing
				{

					Title = "Junior UI/UX Designer",
					Description = "Help us modernize the interface of our global logistics tracking dashboard.",
					CompanyId = 1,
					Location = "Hamburg, Germany",
					JobType = JobTypeEnum.Contract,
					MinSalary = 45000,
					MaxSalary = 60000,
					ExperienceLevel = ExperienceLevelEnum.EntryLevel,
					DatePosted = DateTime.UtcNow.AddDays(-10),
					DateExpired = DateTime.UtcNow.AddDays(20),
					Status = JobStatusEnum.Active,
					CreatedByUserId = 1,
					JobSkills = new List<JobSkill>(),
					jobCategories = new List<JobCategory>()
				},
				new JobListing
				{

					Title = "Data Privacy Specialist",
					Description = "Ensure our healthcare diagnostic tools comply with international data protection laws.",
					CompanyId = 1,
					Location = "Toronto, Canada",
					JobType = JobTypeEnum.FullTime,
					MinSalary = 90000,
					MaxSalary = 130000,
					ExperienceLevel = ExperienceLevelEnum.MidLevel,
					DatePosted = DateTime.UtcNow.AddDays(-20),
					DateExpired = DateTime.UtcNow.AddDays(-5), // Expired job
					Status = JobStatusEnum.Closed,
					CreatedByUserId = 1,
					JobSkills = new List<JobSkill>(),
					jobCategories = new List<JobCategory>()
				}
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
