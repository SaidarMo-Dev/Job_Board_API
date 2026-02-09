using JobBoard.Data.Entities;
using JobBoard.Data.Responses;

namespace JobBoard.Service.Abstractions
{
	public interface IApplicationService
	{
		/// <summary>
		/// Retrieves an <see cref="Application"/> by its unique identifier.
		/// </summary>
		/// <param name="Id">The ID of the <see cref="Application"/>.</param>
		/// <returns>The <see cref="Application"/> if found; otherwise, null.</returns>
		Task<Application> GetByIdAsync(int Id);

		/// <summary>
		/// Retrieves an <see cref="Application"/> by its ID, including related entities (e.g., <c>User</c>, <c>Job</c>).
		/// </summary>
		/// <param name="Id">The ID of the <see cref="Application"/>.</param>
		/// <returns>The <see cref="Application"/> with included related data if found; otherwise, null.</returns>
		Task<Application> GetByIdWithIncludeAsync(int Id);

		/// <summary>
		/// Adds a new <see cref="Application"/> to the database.
		/// </summary>
		/// <param name="application">The <see cref="Application"/> to add.</param>
		/// <returns><c>true</c> if the <paramref name="application"/> was successfully added; otherwise, <c>false</c>.</returns>
		Task<bool> AddAsync(Application application);

		/// <summary>
		/// Updates an existing <see cref="Application"/>.
		/// </summary>
		/// <param name="application">The <see cref="Application"/> with updated data.</param>
		/// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
		Task<bool> UpdateAsync(Application application);

		/// <summary>
		/// Checks if a user has any active or accepted <see cref="Application"/>.
		/// </summary>
		/// <param name="UserId">The ID of the user.</param>
		/// <returns><c>true</c> if the user with ID <paramref name="UserId"/> has an active or accepted <see cref="Application"/>; otherwise, <c>false</c>.</returns>
		Task<bool> HasActiveOrAcceptedApplicationWithJobAsync(int UserId, int jobId);

		/// <summary>
		/// Deletes an existing <see cref="Application"/> from the database.
		/// </summary>
		/// <param name="app">The <see cref="Application"/> to delete.</param>
		/// <returns><c>true</c> if the <paramref name="app"/> was successfully deleted; otherwise, <c>false</c>.</returns>
		Task<bool> DeleteAsync(Application app);


		/// <summary>
		/// Deletes an existing <see cref="Application"/> from the database.
		/// </summary>
		/// <param name="applicationId">The Application Id to delete.</param>
		Task DeleteByIdAsync(int applicationId);

		/// <summary>
		/// Retrieves all <see cref="Application"/> entities associated with a specific job.
		/// </summary>
		/// <param name="JobId">The ID of the job.</param>
		/// <returns>A list of <see cref="Application"/> objects related to the specified job.</returns>
		Task<List<Application>> GetApplicationsByJobIdAsync(int JobId);

		/// <summary>
		/// Retrieves a queryable collection of applications submitted by a specific user.
		/// </summary>
		/// <param name="userId">The ID of the user whose applications are to be retrieved.</param>
		/// <returns>
		/// An <see cref="IQueryable{T}"/> of <see cref="Application"/> objects associated with the specified user.
		/// </returns>
		IQueryable<Application> GetUserApplicationsQueryable(int userId);

		/// <summary>
		/// Retrieves a collection of recent applications submitted by a specific user.
		/// </summary>
		/// <param name="userId">The ID of the user whose applications are to be retrieved.</param>
		/// <param name="take">The number of applications to be retrieves.</param>
		/// <returns>
		/// An <see cref="IReadOnlyList{T}"/> of <see cref="RecentApplicationsResponse"/> objects associated with the specified user.
		/// </returns>
		Task<IReadOnlyList<RecentApplicationsResponse>> GetRecentApplicationsAsync(int userId, int take);

		/// <summary>
		/// Retrieves an array of applied job IDs of a specific user.
		/// </summary>
		/// <param name="userId">The ID of the user whose applied job IDs to be retrieved.</param>
		/// <returns>
		/// An <see cref="IReadOnlyList{T}"/> of <see cref="RecentApplicationsResponse"/> objects associated with the specified user.
		/// </returns>
		Task<int[]> GetAppliedJobIds(int userId);

		Task AttachResumeAsync(int applicationId, int resumeFileId);

	}
}
