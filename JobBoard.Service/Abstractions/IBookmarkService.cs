using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IBookmarkService
	{
		Task<Bookmark> AddAsync(Bookmark bookMark);
		Task<Bookmark> GetBookmarkByIdWithIncludeAsync(int Id);
		Task<Bookmark> GetBookmarkByIdAsync(int Id);
		Task<bool> DeleteByIdAsync(Bookmark bookmark);
		Task<List<Bookmark>> GetUserBookmarks(int UserId);
		IQueryable<Bookmark> GetBookmarksQueryable();
	}
}
