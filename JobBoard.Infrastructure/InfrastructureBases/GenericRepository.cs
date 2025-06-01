using JobBoard.Infrastructure.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace JobBoard.Infrastructure.InfrastructureBases
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		#region fields 
		private readonly appDbContext _context;
		#endregion

		#region constructors
		public GenericRepository(appDbContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public async Task<ICollection<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>().ToListAsync();
		}
		public async Task<TEntity> AddAsync(TEntity entity)
		{
			await _context.Set<TEntity>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
		{
			await _context.Set<TEntity>().AddRangeAsync(entities);
			await _context.SaveChangesAsync();
			return entities;
		}

		public async Task DeleteAsync(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteRangeAsync(ICollection<TEntity> entities)
		{
			_context.Set<TEntity>().RemoveRange(entities);
			await _context.SaveChangesAsync();
		}
		public async Task<TEntity> FindByIdAsync(int id) => await _context.Set<TEntity>().FindAsync(id);

		public async Task<TEntity> FindAsync(Func<TEntity, bool> predicate) => await _context.Set<TEntity>().FindAsync(predicate);

		public IQueryable<TEntity> GetTableAsNoTracking() => _context.Set<TEntity>().AsNoTracking();

		public IQueryable<TEntity> GetTableAsTracking() => _context.Set<TEntity>().AsTracking();

		public async Task UpdateAsync(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
			await _context.SaveChangesAsync();
		}

		public IDbContextTransaction BeginTransaction()
		{
			return _context.Database.BeginTransaction();
		}

		public async void Commit()
		{
			await _context.Database.CommitTransactionAsync();
		}

		public async void RollBack()
		{
			await _context.Database.RollbackTransactionAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void CommitAsync()
		{
			throw new NotImplementedException();
		}

		public void RollBackasync()
		{
			throw new NotImplementedException();
		}
		#endregion


	}
}
