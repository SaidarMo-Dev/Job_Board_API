using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IJobSkillService
	{
		/// <summary>
		/// Adds a new <see cref="JobSkill"/> entity asynchronously.
		/// </summary>
		/// <param name="entity">The <see cref="JobSkill"/> entity to add.</param>
		/// <returns>A string indicating the result of the add operation.</returns>
		Task<string> AddAsync(JobSkill entity);

		/// <summary>
		/// Adds a collection of <see cref="JobSkill"/> entities asynchronously.
		/// </summary>
		/// <param name="entities">The collection of <see cref="JobSkill"/> entities to add.</param>
		/// <returns>A task representing the asynchronous add operation.</returns>
		Task AddRangeAsync(ICollection<JobSkill> entities);

		/// <summary>
		/// Deletes all <see cref="JobSkill"/> entities associated with a specific job ID asynchronously.
		/// </summary>
		/// <param name="JobID">The ID of the job whose skills should be deleted.</param>
		/// <returns>A task representing the asynchronous delete operation.</returns>
		Task DeleteJobSkillsAsync(int JobID);

		/// <summary>
		/// Checks if a <see cref="JobSkill"/> exists for the specified job ID and skill ID.
		/// </summary>
		/// <param name="JobId">The ID of the job.</param>
		/// <param name="SkillId">The ID of the skill.</param>
		/// <returns><c>true</c> if the <see cref="JobSkill"/> exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistById(int JobId, int SkillId);

		/// <summary>
		/// Deletes the specified <see cref="JobSkill"/> entity asynchronously.
		/// </summary>
		/// <param name="jobSkill">The <see cref="JobSkill"/> entity to delete.</param>
		/// <returns>A task representing the asynchronous delete operation.</returns>
		Task DeleteAsync(JobSkill jobSkill);

	}
}
