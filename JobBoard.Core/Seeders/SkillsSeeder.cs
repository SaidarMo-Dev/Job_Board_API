using JobBoard.Data.Entities;
using JobBoard.Infrastructure.context;

public static class SkillSeeder
{
	public static async Task SeedAsync(appDbContext context)
	{
		if (context.skills.Any()) return;

		var skills = new List<Skill>
		{
			new Skill { Name = "C#",NormalizedName= "C#", Description = "A modern, object-oriented programming language developed by Microsoft." },
			new Skill { Name = "JavaScript",NormalizedName = "JAVASCRIPT", Description = "A scripting language primarily used for creating interactive web interfaces." },
			new Skill { Name = "SQL",NormalizedName= "SQL", Description = "A language used for querying and managing data in relational databases." },
			new Skill { Name = "React",NormalizedName ="REACT", Description = "A JavaScript library for building fast and interactive user interfaces." },
			new Skill { Name = "ASP.NET Core", NormalizedName = "ASP.NET Core", Description = "A cross-platform, high-performance framework for building modern web applications." },
			new Skill { Name = "Flutter", NormalizedName = "FlUTTER", Description = "A cross-platform, high-performance framework for building modern mobile applications." },
		};

		await context.skills.AddRangeAsync(skills);
		await context.SaveChangesAsync();
	}
}
