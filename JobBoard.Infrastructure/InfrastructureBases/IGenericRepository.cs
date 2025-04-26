using Microsoft.EntityFrameworkCore.Storage;

namespace JobBoard.Infrastructure.InfrastructureBases
{
	public interface IGenericRepository<TEntity>
	{
		Task<ICollection<TEntity>> GetAllAsync();
		IQueryable<TEntity> GetTableAsNoTracking();
		IQueryable<TEntity> GetTableAsTracking();
		Task<TEntity> FindByIdAsync(int id);
		Task<TEntity> FindAsync(Func<TEntity, bool> predicate);
		Task<TEntity> AddAsync(TEntity entity);
		Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);
		Task DeleteAsync(TEntity entity);
		Task DeleteRangeAsync(ICollection<TEntity> entities);
		Task UpdateAsync(TEntity entity);
		IDbContextTransaction BeginTransaction();
		void Commit();
		void RollBack();

		public Task SaveChangesAsync();
	}
}
