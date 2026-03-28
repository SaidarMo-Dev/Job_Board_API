using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Extentions.Queries.Companies
{
	public static class CompanySearchExtensions
	{
		/// <summary>
		/// Applies search to a company query.
		/// </summary>
		/// <param name="query">The base IQueryable of companies</param>
		/// <param name="search">Search term for name or description</param>
		/// <returns>Filtered IQueryable</returns>
		public static IQueryable<Company> ApplyCompanySearch(
			this IQueryable<Company> query,
			string? search = null)
		{
			if (string.IsNullOrWhiteSpace(search)) return query;

			// We wrap the search term in % wildcards manually
			var pattern = $"%{search.Trim().ToLower()}%";

			return query.Where(c =>
				EF.Functions.Like(c.CompanyName, pattern) ||
				EF.Functions.Like(c.City, pattern) ||
				EF.Functions.Like(c.Industry, pattern) ||
				EF.Functions.Like(c.Address, pattern) ||
				EF.Functions.Like(c.Description, pattern));
		}
	}
}
