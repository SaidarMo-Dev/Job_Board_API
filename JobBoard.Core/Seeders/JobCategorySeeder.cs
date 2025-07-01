using JobBoard.Data.Entities;
using JobBoard.Infrastructure.context;

namespace JobBoard.Core.Seeders
{
	public static class JobCategorySeeder
	{
		public static async Task SeedAsync(appDbContext context)
		{
			if (context.jobCategories.Any()) return;

			var jobCategoryLinks = new List<JobCategory>
		{
			new JobCategory { JobListingId = 1, CategoryId = 1 }, // Mobile Developer → Software Development
            new JobCategory { JobListingId = 2, CategoryId = 1 }, // QA Engineer
            new JobCategory { JobListingId = 3, CategoryId = 5 }, // DevOps → IT & Networking
            new JobCategory { JobListingId = 4, CategoryId = 4 }, // Project Manager → Marketing
            new JobCategory { JobListingId = 5, CategoryId = 5 }, // IT Support
            new JobCategory { JobListingId = 6, CategoryId = 5 }, // Cloud Architect
            new JobCategory { JobListingId = 7, CategoryId = 5 }, // SysAdmin
            new JobCategory { JobListingId = 8, CategoryId = 2 }, // Content Writer → Design & Creative
            new JobCategory { JobListingId = 9, CategoryId = 3 }, // Cybersecurity → Data
            new JobCategory { JobListingId = 10, CategoryId = 3 }, // AI
            new JobCategory { JobListingId = 11, CategoryId = 4 }, // Business Analyst
            new JobCategory { JobListingId = 12, CategoryId = 4 }, // Recruiter
            new JobCategory { JobListingId = 13, CategoryId = 3 }, // ML
            new JobCategory { JobListingId = 14, CategoryId = 3 }, // DBA
            new JobCategory { JobListingId = 15, CategoryId = 4 }, // Product Manager
        };

			await context.jobCategories.AddRangeAsync(jobCategoryLinks);
			await context.SaveChangesAsync();
		}
	}
}