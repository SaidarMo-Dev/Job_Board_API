using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class CountryRepository : ICountryRepository
	{
		#region fields
		private readonly DbSet<Country> _countries;
		#endregion

		#region constructors
		public CountryRepository(appDbContext context)
		{
			_countries = context.countries;
		}

		#endregion

		#region methods

		public async Task<ICollection<Country>> GetAllAsync()
		{
			return await _countries.AsNoTracking().ToListAsync();
		}

		public async Task<Country> GetCountryByIdAsyn(int id)
		{
			return await _countries.FindAsync(id);
		}

		public IQueryable<Country> GetTableAsNoTracking()
		{
			return _countries.AsNoTracking();
		}

		public IQueryable<Country> GetTableAsTracking()
		{
			return _countries.AsTracking();
		}

		#endregion

	}
}
