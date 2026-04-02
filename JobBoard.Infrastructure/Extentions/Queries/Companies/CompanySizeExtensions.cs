using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Infrastructure.Extentions.Queries.Companies
{
	public static class CompanySizeExtensions
	{
		/// <summary>
		/// Filters companies based on the CompanySize enum mapping to DB strings.
		/// </summary>
		public static IQueryable<Company> WhereCompanySizeIs(this IQueryable<Company> query, CompanySize[]? sizes)
		{
			if (sizes == null || sizes.Length == 0)
				return query;

			var sizeStrings = sizes
				.Select(s => s switch
				{
					CompanySize.Small => "0-50",
					CompanySize.Medium => "51-500",
					CompanySize.Large => "500+",
					_ => null
				})
				.Where(s => s != null)
				.ToArray();

			if (sizeStrings.Length == 0)
				return query;

			return query.Where(c => sizeStrings.Contains(c.CompanySize));
		}

	}
}
