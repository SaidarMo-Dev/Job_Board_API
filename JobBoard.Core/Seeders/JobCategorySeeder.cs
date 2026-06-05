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
           
        };

			await context.jobCategories.AddRangeAsync(jobCategoryLinks);
			await context.SaveChangesAsync();
		}
	}
}