using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IJobCategoryService
	{
		/// <summary>
		/// Adds a collection of <see cref="JobCategory"/> entities asynchronously.
		/// </summary>
		/// <param name="entities">The collection of <see cref="JobCategory"/> entities to add.</param>
		/// <returns><c>true</c> if the entities were added successfully; otherwise, <c>false</c>.</returns>
		Task<bool> AddRangeAsync(ICollection<JobCategory> entities);

		/// <summary>
		/// Adds a single <see cref="JobCategory"/> entity asynchronously.
		/// </summary>
		/// <param name="jobCategory">The <see cref="JobCategory"/> entity to add.</param>
		/// <returns>A task representing the asynchronous add operation.</returns>
		Task AddAsync(JobCategory jobCategory);

		/// <summary>
		/// Deletes a specified <see cref="JobCategory"/> entity asynchronously.
		/// </summary>
		/// <param name="jobCategory">The <see cref="JobCategory"/> entity to delete.</param>
		/// <returns>A task representing the asynchronous delete operation.</returns>
		Task DeleteAsync(JobCategory jobCategory);

		/// <summary>
		/// Checks if a <see cref="JobCategory"/> exists by job ID and category ID.
		/// </summary>
		/// <param name="jobId">The ID of the job.</param>
		/// <param name="categoryId">The ID of the category.</param>
		/// <returns><c>true</c> if the <see cref="JobCategory"/> exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistById(int jobId, int categoryId);

	}
}
