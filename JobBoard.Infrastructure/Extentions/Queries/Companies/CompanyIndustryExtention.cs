using JobBoard.Data.Entities;

namespace JobBoard.Infrastructure.Extentions.Queries.Companies
{
	public static class CompanyIndustryExtention
	{
		public static IQueryable<Company> WhereIndustriesIn(
				this IQueryable<Company> query,
				string[]? industrySlugs)
		{
			if (industrySlugs == null || industrySlugs.Length == 0)
				return query;

			// Clean input
			var slugs = industrySlugs
				.Where(s => !string.IsNullOrWhiteSpace(s))
				.Select(s => s.Trim())
				.ToArray();

			if (slugs.Length == 0)
				return query;

			return query.Where(c =>
				c.CompanyIndustries.Any(ci =>
					slugs.Contains(ci.Industry.Slug.ToLower())
				));
		}
	}
}
