using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface ICategoryService
	{
		Task<Category> FindById(int Id);
		Task<ICollection<Category>> GetAllAsync();
		IQueryable<Category> GetJobCategories(int JobId);
		Task<bool> IsExistByIdAsync(int Id);
		bool IsExistById(int Id);
		Task<bool> IsNameExistAsync(string Name);
		Task<bool> IsNameExistExcludeSelfAsync(int Id, string Name);
		Task<int> AddAsync(Category category);
		Task<string> UpdateAsync(Category category);
		Task<string> DeleteAsync(Category category);

	}
}
