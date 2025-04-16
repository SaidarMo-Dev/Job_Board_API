using Microsoft.EntityFrameworkCore;

namespace JobBoard.Core.Wrapers
{
	public static class PaginateExtention
	{
		public static async Task<PaginatedResponse<List<T>>> ToPaginatedAsync<T>(this IQueryable<T> source, int PageNumber = 1, int PageSize = 10)
		{
			if (source == null) throw new ArgumentNullException("Source ds empty");

			PageNumber = PageNumber <= 0 ? 1 : PageNumber;
			PageSize = PageSize <= 0 ? 10 : PageSize;
			int count = await source.CountAsync();
			if (count == 0) return PaginatedResponse<List<T>>.Success(null!, PageNumber, PageSize, count);

			var result = await source.AsQueryable<T>().Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();

			return PaginatedResponse<List<T>>.Success(result, PageNumber, PageSize, count);

		}
	}
}
