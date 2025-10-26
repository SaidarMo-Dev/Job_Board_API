using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Service.Abstractions
{
	public interface ICategoryService
	{
		/// <summary>
		/// Finds a category by its ID asynchronously.
		/// </summary>
		/// <param name="Id">The ID of the category.</param>
		/// <returns>The <see cref="Category"/> entity if found; otherwise, null.</returns>
		Task<Category> FindById(int Id);

		/// <summary>
		/// Retrieves all categories asynchronously.
		/// </summary>
		/// <returns>A collection of all <see cref="Category"/> entities.</returns>
		Task<ICollection<Category>> GetAllAsync();

		/// <summary>
		/// Retrieves all categories associated with a specific job.
		/// </summary>
		/// <param name="JobId">The ID of the job.</param>
		/// <returns>An <see cref="IQueryable{Category}"/> of related categories.</returns>
		IQueryable<Category> GetJobCategories(int JobId);

		/// <summary>
		/// Checks asynchronously if a category exists by its ID.
		/// </summary>
		/// <param name="Id">The ID of the category.</param>
		/// <returns><c>true</c> if the category exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByIdAsync(int Id);

		/// <summary>
		/// Checks if a category exists by its ID.
		/// </summary>
		/// <param name="Id">The ID of the category.</param>
		/// <returns><c>true</c> if the category exists; otherwise, <c>false</c>.</returns>
		bool IsExistById(int Id);

		/// <summary>
		/// Checks asynchronously if a category name already exists.
		/// </summary>
		/// <param name="Name">The name of the category.</param>
		/// <returns><c>true</c> if the name exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsNameExistAsync(string Name);

		/// <summary>
		/// Checks asynchronously if a category name exists, excluding the specified category by ID.
		/// Useful for validating uniqueness during updates.
		/// </summary>
		/// <param name="Id">The ID of the category to exclude.</param>
		/// <param name="Name">The name to check for existence.</param>
		/// <returns><c>true</c> if the name exists in another category; otherwise, <c>false</c>.</returns>
		Task<bool> IsNameExistExcludeSelfAsync(int Id, string Name);

		/// <summary>
		/// Adds a new category asynchronously.
		/// </summary>
		/// <param name="category">The <see cref="Category"/> to add.</param>
		/// <returns>The ID of the newly added category.</returns>
		Task<int> AddAsync(Category category);

		/// <summary>
		/// Updates an existing category asynchronously.
		/// </summary>
		/// <param name="category">The <see cref="Category"/> to update.</param>
		/// <returns>A status message indicating the result of the update.</returns>
		Task<string> UpdateAsync(Category category);

		/// <summary>
		/// Deletes a category asynchronously.
		/// </summary>
		/// <param name="category">The <see cref="Category"/> to delete.</param>
		/// <returns>A status message indicating the result of the deletion.</returns>
		Task<string> DeleteAsync(Category category);

		/// <summary>
		/// Get categories queryable
		/// </summary>
		/// <param name="search"> the name to search by.</param>
		/// <param name="sort"> the enum <see cref="SortCategory"/> to sort categories.</param>
		/// <returns>queryable categories</returns>
		IQueryable<Category> GetCategoriesQueryable(string? search, SortCategory? sort);

		Task<List<Category>> GetPopularCategoriesAsync();
	}
}
