using JobBoard.Data.Entities;
using JobBoard.Infrastructure.context;

namespace JobBoard.Core.Seeders
{
	public static class CategorySeeder
	{
		public static async Task SeedAsync(appDbContext context)
		{
			if (context.categories.Any()) return;

			var categories = new List<Category>
		{
			new Category { Name = "Software Development", Description = "Jobs related to application, system, and web development." },
			new Category { Name = "Design & Creative", Description = "UI/UX design, graphic design, and other creative roles." },
			new Category { Name = "Data & Analytics", Description = "Data science, machine learning, and data engineering roles." },
			new Category { Name = "Marketing", Description = "Digital marketing, SEO, content strategy, and brand management." },
			new Category { Name = "IT & Networking", Description = "System administration, IT support, and network engineering jobs." },
		};

			await context.categories.AddRangeAsync(categories);
			await context.SaveChangesAsync();
		}
	}
}