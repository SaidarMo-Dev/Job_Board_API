using JobBoard.Data.Entities;
using JobBoard.Infrastructure.context;

namespace JobBoard.Core.Seeders
{
	public static class CompanySeeder
	{

		private static List<Company> GetCompanies()
		{
			return new List<Company>
			{
				new Company
				{

					CompanyName = "Nebula Stream",
					Slug = "nebula-stream",
					Description = "A next-generation real-time data processing platform for IoT devices.",
					ShortDescription = "Real-time IoT data solutions.",
					Industry = "Technology",
					CompanySize = "11-50",
					FoundedYear = 2022,
					WebsiteUrl = "https://nebula-stream.io",
					LinkedInUrl = "https://linkedin.com/company/nebulastream",
					Email = "contact@nebula-stream.io",
					Country = "USA",
					City = "San Francisco",
					Address = "101 California St, Ste 2710",
					Location = "San Francisco, CA",
					IsFeatured = true,
					IsVerified = true,
					CreatedAt = DateTime.UtcNow.AddMonths(-6),
					CreatedByUserId = 1
				}
			};

		}
		public static async Task SeedAsync(appDbContext dbContext)
		{
			var companies = dbContext.companies.Count();
			if (companies == 0)
			{
				dbContext.companies.AddRange(GetCompanies());
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
