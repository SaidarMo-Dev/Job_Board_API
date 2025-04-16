using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface ICategoryService
	{
		Task<Category> GetCategoryByIdAsync(int Id);
		Task<ICollection<Category>> GetAllAsync();
		IQueryable<Category> GetJobCategories(int JobId);
	}
}
