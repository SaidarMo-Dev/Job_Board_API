using Microsoft.EntityFrameworkCore.Storage;

namespace JobBoard.Infrastructure.InfrastructureBases
{
	public interface IGenericRepository<TEntity>
	{
		/// <summary>
		/// Retrieves all entities asynchronously.
		/// </summary>
		/// <returns>A collection of all entities.</returns>
		Task<ICollection<TEntity>> GetAllAsync();

		/// <summary>
		/// Retrieves the entity table with no tracking (read-only).
		/// </summary>
		/// <returns>An <see cref="IQueryable{TEntity}"/> without tracking.</returns>
		IQueryable<TEntity> GetTableAsNoTracking();

		/// <summary>
		/// Retrieves the entity table with tracking enabled.
		/// </summary>
		/// <returns>An <see cref="IQueryable{TEntity}"/> with tracking.</returns>
		IQueryable<TEntity> GetTableAsTracking();

		/// <summary>
		/// Finds an entity by its identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <returns>The entity if found; otherwise, null.</returns>
		Task<TEntity> FindByIdAsync(int id);

		/// <summary>
		/// Finds an entity that matches the specified predicate asynchronously.
		/// </summary>
		/// <param name="predicate">A function to test each entity for a condition.</param>
		/// <returns>The matching entity if found; otherwise, null.</returns>
		Task<TEntity> FindAsync(Func<TEntity, bool> predicate);

		/// <summary>
		/// Adds a new entity asynchronously.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		/// <returns>The added entity.</returns>
		Task<TEntity> AddAsync(TEntity entity);

		/// <summary>
		/// Adds a range of new entities asynchronously.
		/// </summary>
		/// <param name="entities">The collection of entities to add.</param>
		/// <returns>The added entities.</returns>
		Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

		/// <summary>
		/// Deletes an entity asynchronously.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		Task DeleteAsync(TEntity entity);

		/// <summary>
		/// Deletes a range of entities asynchronously.
		/// </summary>
		/// <param name="entities">The collection of entities to delete.</param>
		Task DeleteRangeAsync(ICollection<TEntity> entities);

		/// <summary>
		/// Updates an existing entity asynchronously.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		Task UpdateAsync(TEntity entity);

		/// <summary>
		/// Begins a new database transaction.
		/// </summary>
		/// <returns>A database transaction instance.</returns>
		IDbContextTransaction BeginTransaction();

		/// <summary>
		/// Commits the current transaction.
		/// </summary>
		void Commit();

		/// <summary>
		/// Rolls back the current transaction.
		/// </summary>
		void RollBack();

		/// <summary>
		/// Commits the current transaction asynchronously.
		/// </summary>
		void CommitAsync();

		/// <summary>
		/// Rolls back the current transaction asynchronously.
		/// </summary>
		void RollBackasync();

		/// <summary>
		/// Saves all changes made in this context to the database asynchronously.
		/// </summary>
		/// <returns>A task representing the asynchronous save operation.</returns>
		Task SaveChangesAsync();

	}
}
