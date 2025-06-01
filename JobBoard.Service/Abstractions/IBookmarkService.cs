using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IBookmarkService
	{
		/// <summary>
		/// Adds a new bookmark asynchronously.
		/// </summary>
		/// <param name="bookMark">The bookmark entity to add.</param>
		/// <returns>The added <see cref="Bookmark"/> entity.</returns>
		Task<Bookmark> AddAsync(Bookmark bookMark);

		/// <summary>
		/// Retrieves a bookmark by its ID with related entities included, asynchronously.
		/// </summary>
		/// <param name="Id">The ID of the bookmark.</param>
		/// <returns>The <see cref="Bookmark"/> entity with its related data, if found; otherwise, null.</returns>
		Task<Bookmark> GetBookmarkByIdWithIncludeAsync(int Id);

		/// <summary>
		/// Retrieves a bookmark by its ID asynchronously.
		/// </summary>
		/// <param name="Id">The ID of the bookmark.</param>
		/// <returns>The <see cref="Bookmark"/> entity if found; otherwise, null.</returns>
		Task<Bookmark> GetBookmarkByIdAsync(int Id);

		/// <summary>
		/// Deletes the specified bookmark asynchronously.
		/// </summary>
		/// <param name="bookmark">The <see cref="Bookmark"/> entity to delete.</param>
		/// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
		Task<bool> DeleteByIdAsync(Bookmark bookmark);

		/// <summary>
		/// Retrieves a list of bookmarks for a specific user.
		/// </summary>
		/// <param name="UserId">The ID of the user.</param>
		/// <returns>A list of <see cref="Bookmark"/> entities associated with the user.</returns>
		Task<List<Bookmark>> GetUserBookmarks(int UserId);

		/// <summary>
		/// Retrieves a queryable collection of bookmarks.
		/// </summary>
		/// <returns>An <see cref="IQueryable{Bookmark}"/> for further querying operations.</returns>
		IQueryable<Bookmark> GetBookmarksQueryable();

	}
}
