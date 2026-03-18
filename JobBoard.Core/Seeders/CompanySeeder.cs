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
					CompanyName = "TechNovaxy Inc",
					Description = "A leading software company focused on enterprise-grade solutions and cloud infrastructure.",
					WebsiteUrl = "https://www.technovaxy.com",
					Location = "New York, NY",
					PhoneNumber = "212-555-1001",
					Email = "info@technovaxy.com",
					Fax = "212-555-1002",
					CreatedByUserId = 1,
					Slug = "tech-novaxy"
				},
				new Company
				{

					CompanyName = "Designity Solutions",
					Description = "Creative agency delivering high-end UX/UI design, branding, and visual identity services.",
					WebsiteUrl = "https://www.designity.com",
					Location = "Los Angeles, CA",
					PhoneNumber = "310-555-2001",
					Email = "contact@designify.com",
					Fax = "310-555-2002",
					CreatedByUserId = 1,
					Slug = "designity"
				},
				new Company
				{

					CompanyName = "DataSolve Corp Df",
					Description = "Analytics and AI-driven consulting company solving complex business data problems globally.",
					WebsiteUrl = "https://www.datasolvedf.com",
					Location = "Chicago, IL",
					PhoneNumber = "312-555-3001",
					Email = "support@datasolvedf.com",
					Fax = "312-555-3002",
					CreatedByUserId = 1,
					Slug = "data-solve"
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
