using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Service.Abstractions
{
	public interface ICompanyService
	{
		Task<Company> GetCompanyByIdAsync(int Id);
		Task<ICollection<Company>> GetAllAsync();
		IQueryable<Company> GetPaginatedQueryable();
		IQueryable<Company> FilterPaginatedQueryable(OrderCompanyEnum order);
		Task AddAsync(Company entity);
		Task UpdateAsync(Company entity);
		Task<bool> IsExistByNameAsync(string companyName);
		Task<bool> IsExistByNameExcludeSelfAsync(int Id, string companyName);
		Task<bool> IsExistByIdAsync(int id);
		Task DeleteAsync(Company entity);


	}
}
