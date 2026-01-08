using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IFileResourceService
	{
		/// <summary>
		/// Adds a new fileResource asynchronously.
		/// </summary>
		/// <param name="fileResource">The fileResource entity to add.</param>
		/// <returns>The added <see cref="FileResource"/> entity.</returns>
		Task<FileResource> AddAsync(FileResource fileResource);

		/// <summary>
		/// Retrieves a fileResource by its ID asynchronously.
		/// </summary>
		/// <param name="Id">The ID of the fileResource.</param>
		/// <returns>The <see cref="FileResource"/> entity if found; otherwise, null.</returns>
		Task<FileResource> GetByIdAsync(int Id);

		/// <summary>
		/// Updates an existing fileResource asynchronously.
		/// </summary>
		/// <param name="fileResource">The <see cref="FileResource"/> entity to update.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task<FileResource> UpdateAsync(FileResource fileResource);

		/// <summary>
		/// Deletes the specified fileResource asynchronously.
		/// </summary>
		/// <param name="fileResource">The <see cref="FileResource"/> entity to delete.</param>
		/// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
		Task<bool> DeleteAsync(FileResource fileResource);

	}
}
