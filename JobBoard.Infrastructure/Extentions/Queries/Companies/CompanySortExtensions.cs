using System.Linq.Expressions;
using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Infrastructure.Extentions.Queries.Companies
{
	public static class CompanySortExtensions
	{
		public static IQueryable<Company> ApplyCompanySorting(
			this IQueryable<Company> query,
			CompanySortBy sortBy,
			SortDirection direction)
		{
			// 1. Determine the property to sort by
			Expression<Func<Company, object>> keySelector = sortBy switch
			{
				CompanySortBy.CreatedAt => c => c.CreatedAt,
				CompanySortBy.CompanySize => c => c.CompanySize!,
				_ => c => c.CompanyName // Default to Name
			};

			// 2. Apply the direction
			return direction == SortDirection.Ascending
				? query.OrderBy(keySelector)
				: query.OrderByDescending(keySelector);
		}
	}
}
