using JobBoard.Data.Entities;
using JobBoard.Infrastructure.context;

public static class JobSkillSeeder
{
	public static async Task SeedAsync(appDbContext context)
	{
		if (context.jobSkills.Any()) return;

		var jobSkillLinks = new List<JobSkill>
		{
			new JobSkill { JobListingId = 1, SkillId = 1 }, // Mobile Developer → C#
            new JobSkill { JobListingId = 1, SkillId = 5 }, // + ASP.NET Core
            new JobSkill { JobListingId = 2, SkillId = 3 }, // QA Engineer → SQL
            new JobSkill { JobListingId = 3, SkillId = 1 }, // DevOps Engineer → C#
            new JobSkill { JobListingId = 3, SkillId = 3 }, // + SQL
           
        };

		await context.jobSkills.AddRangeAsync(jobSkillLinks);
		await context.SaveChangesAsync();
	}
}
