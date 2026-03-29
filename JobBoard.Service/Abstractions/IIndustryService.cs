using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IIndustryService
	{
		Task<Industry> GetByIdAsync(int id);
		IQueryable<Industry> GetIndustriesQueryable();
		Task AddAsync(Industry entity);
		Task UpdateAsync(Industry entity);
		Task DeleteAsync(Industry entity);
	}

}
