using JobBoard.Data.Entities;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Service.Abstractions
{
	public interface IJobService
	{
		/// <summary>
		/// Adds a new job listing to the Job table asynchronously.
		/// </summary>
		/// <param name="entity">The <see cref="JobListing"/> object representing the job to add.</param>
		/// <returns>A <see cref="string"/> representing if the job added Successfully or not </returns>

		Task<string> AddNewJobAsync(JobListing entity);

		/// <summary>
		/// Retrieves a job listing by its unique identifier, including related entities.
		/// </summary>
		/// <param name="Id">The unique identifier of the job to retrieve.</param>
		/// <returns>A <see cref="JobListing"/> object representing the job along with its related data, or null if not found.</returns>
		Task<JobListing> GetJobByIdWithEncludeAsync(int Id);

		/// <summary>
		/// Retrieves a job listing by its unique identifier, including related skills and categories.
		/// </summary>
		/// <param name="Id">The unique identifier of the job to retrieve.</param>
		/// <returns>A <see cref="JobListing"/> object representing the job with its associated skills and categories, or null if not found.</returns>

		Task<JobListing> GetJobByIdWithEncludeSkillsAndCategoriesAsync(int Id);

		/// <summary>
		/// Retrieves a job listing by its unique identifier asynchronously.
		/// </summary>
		/// <param name="Id">The unique identifier of the job to retrieve.</param>
		/// <returns>A <see cref="JobListing"/> object representing the job with the specified ID.</returns>
		Task<JobListing> GetJobByIdAsync(int Id);
		/// <summary>
		/// Retrieves an <see cref="IQueryable{JobListing}"/> representing all job listings.
		/// </summary>
		/// <returns>An <see cref="IQueryable{JobListing}"/> to allow further query composition.</returns>
		IQueryable<JobListing> GetJobsQueryable();

		/// <summary>
		/// Retrieves the list of skills associated with a specific job.
		/// </summary>
		/// <param name="JobId">The unique identifier of the job.</param>
		/// <returns>A list of <see cref="Skill"/> objects related to the job.</returns>
		Task<List<Skill>> GetJobSkillsAsync(int JobId);

		/// <summary>
		/// Retrieves the list of categories associated with a specific job.
		/// </summary>
		/// <param name="JobId">The unique identifier of the job.</param>
		/// <returns>A list of <see cref="Category"/> objects related to the job.</returns>
		Task<List<Category>> GetJobCategoriesAsync(int JobId);

		/// <summary>
		/// Updates an existing job listing asynchronously.
		/// </summary>
		/// <param name="job">The <see cref="JobListing"/> object containing updated job information.</param>
		/// <returns>The updated <see cref="JobListing"/> object.</returns>
		Task<JobListing> UpdateAsync(JobListing job);

		/// <summary>
		/// Deletes a specified job listing asynchronously.
		/// </summary>
		/// <param name="job">The <see cref="JobListing"/> object to delete.</param>
		/// <returns><see langword="true"/> if the job was successfully deleted; otherwise, <see langword="false"/>.</returns>
		Task<bool> DeleteJobAsync(JobListing job);

		/// <summary>
		/// Checks if a job listing exists by its unique identifier.
		/// </summary>
		/// <param name="JobId">The unique identifier of the job to check.</param>
		/// <returns><see langword="true"/> if the job exists; otherwise, <see langword="false"/>.</returns>
		Task<bool> IsExistByIdAsync(int JobId);

		/// <summary>
		/// Retrieves a list of job listings associated with the specified company ID.
		/// </summary>
		/// <param name="CompanyId">The unique identifier of the company whose jobs are to be retrieved.</param>
		/// <returns>A list of <see cref="JobListing"/> objects representing the jobs posted by the company.</returns>
		Task<List<JobListing>> GetJobsByCompanyIdAsync(int CompanyId);

		Task<string[]> GetPopularLocations();
		IQueryable<JobListing> GetRecommendationJobs(User user, int take = 3);
	}
}
