using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Infrastructure.Extentions.Queries.Companies
{
	public static class CompanySizeExtensions
	{
		/// <summary>
		/// Filters companies based on the CompanySize enum mapping to DB strings.
		/// </summary>
		public static IQueryable<Company> WhereCompanySizeIs(this IQueryable<Company> query, CompanySize? size)
		{
			if (size == null) return query;

			// Map Enum to the exact string stored in your Database
			string sizeString = size switch
			{
				CompanySize.Small => "0-50",
				CompanySize.Medium => "51-500",
				CompanySize.Large => "500+",
				_ => string.Empty
			};

			return query.Where(c => c.CompanySize! == sizeString);
		}

	}
}
