using JobBoard.Data.Entities;

namespace JobBoard.Infrastructure.Abstractions
{
	public interface ICountryRepository
	{
		Task<ICollection<Country>> GetAllAsync();
		Task<Country> GetCountryByIdAsyn(int id);
		IQueryable<Country> GetTableAsNoTracking();
		IQueryable<Country> GetTableAsTracking();
	}
}
