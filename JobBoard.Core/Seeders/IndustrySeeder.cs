using JobBoard.Data.Entities;
using JobBoard.Infrastructure.context;

namespace JobBoard.Core.Seeders
{
	public static class IndustrySeeder
	{

		private static IEnumerable<Industry> GetIndustries()
		{
			return new List<Industry>
			{
				new Industry { Name =  "Software Development", Slug = "software-development"},
				new Industry { Name = "Information Technology", Slug = "information-technology" },
				new Industry { Name = "Healthcare" , Slug = "healthcare"},
				new Industry { Name = "Finance", Slug = "finance"  },
				new Industry { Name = "Education" , Slug = "education"},
				new Industry { Name = "Manufacturing", Slug = "manufacturing" }
				,

			};
		}

		public static async Task SeedAsync(appDbContext dbContext)
		{
			var industries = dbContext.Industries.Count();
			if (industries == 0)
			{
				dbContext.Industries.AddRange(GetIndustries());
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
