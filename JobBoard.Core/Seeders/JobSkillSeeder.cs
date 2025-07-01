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
            new JobSkill { JobListingId = 4, SkillId = 2 }, // Project Manager → JavaScript
            new JobSkill { JobListingId = 5, SkillId = 3 }, // IT Support → SQL
            new JobSkill { JobListingId = 6, SkillId = 1 }, // Cloud Architect → C#
            new JobSkill { JobListingId = 7, SkillId = 3 }, // System Admin → SQL
            new JobSkill { JobListingId = 8, SkillId = 2 }, // Content Writer → JS
            new JobSkill { JobListingId = 9, SkillId = 4 }, // Cybersecurity → React
            new JobSkill { JobListingId = 10, SkillId = 4 }, // AI Engineer → React
            new JobSkill { JobListingId = 11, SkillId = 2 }, // Business Analyst → JS
            new JobSkill { JobListingId = 12, SkillId = 2 }, // Recruiter → JS
            new JobSkill { JobListingId = 13, SkillId = 4 }, // ML Engineer → React
            new JobSkill { JobListingId = 14, SkillId = 3 }, // DBA → SQL
            new JobSkill   { JobListingId = 15, SkillId = 1 }, // Product Manager → C#
        };

		await context.jobSkills.AddRangeAsync(jobSkillLinks);
		await context.SaveChangesAsync();
	}
}
