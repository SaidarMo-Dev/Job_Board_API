using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface ISkillService
	{
		/// <summary>
		/// Retrieves all skills asynchronously.
		/// </summary>
		/// <returns>A collection of all <see cref="Skill"/> entities.</returns>
		Task<ICollection<Skill>> GetAllAsync();

		/// <summary>
		/// Retrieves a skill by its ID asynchronously.
		/// </summary>
		/// <param name="Id">The ID of the skill.</param>
		/// <returns>The <see cref="Skill"/> entity if found; otherwise, null.</returns>
		Task<Skill> GetSkillByIdAsync(int Id);

		/// <summary>
		/// Adds a new skill asynchronously.
		/// </summary>
		/// <param name="entity">The <see cref="Skill"/> entity to add.</param>
		/// <returns>The added <see cref="Skill"/> entity.</returns>
		Task<Skill> AddNewSkillAsync(Skill entity);

		/// <summary>
		/// Updates an existing skill asynchronously.
		/// </summary>
		/// <param name="entity">The <see cref="Skill"/> entity to update.</param>
		Task UpdateAsnyc(Skill entity);

		/// <summary>
		/// Checks asynchronously if a skill with the given name exists.
		/// </summary>
		/// <param name="name">The name of the skill.</param>
		/// <returns><c>true</c> if a skill with the specified name exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByNameAsync(string name);

		/// <summary>
		/// Checks asynchronously if a skill exists by its ID.
		/// </summary>
		/// <param name="Id">The ID of the skill.</param>
		/// <returns><c>true</c> if the skill exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByIdAsync(int Id);

		/// <summary>
		/// Checks if a skill exists by its ID.
		/// </summary>
		/// <param name="Id">The ID of the skill.</param>
		/// <returns><c>true</c> if the skill exists; otherwise, <c>false</c>.</returns>
		bool IsExistById(int Id);

		/// <summary>
		/// Checks asynchronously if a skill name exists, excluding the specified skill ID.
		/// Useful for validating uniqueness during updates.
		/// </summary>
		/// <param name="Id">The ID of the skill to exclude.</param>
		/// <param name="name">The skill name to check for existence.</param>
		/// <returns><c>true</c> if the name exists in another skill; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByNameExcludeSelfAsync(int Id, string name);

		/// <summary>
		/// Deletes a skill asynchronously.
		/// </summary>
		/// <param name="entity">The <see cref="Skill"/> entity to delete.</param>
		Task DeleteAsync(Skill entity);

		/// <summary>
		/// Retrieves a queryable collection of skills associated with a specific job.
		/// </summary>
		/// <param name="JobId">The ID of the job.</param>
		/// <returns>An <see cref="IQueryable{Skill}"/> for further querying.</returns>
		IQueryable<Skill> GetJobSkills(int JobId);

	}
}
