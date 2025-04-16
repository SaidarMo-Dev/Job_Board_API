using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;

namespace JobBoard.Service.Implementations
{
	public class CategoryService : ICategoryService
	{

		#region Fileds
		private readonly ICategoryRepository _categoryRepository;
		#endregion

		#region Constructors
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		#endregion

		#region Methods

		public async Task<ICollection<Category>> GetAllAsync()
		{
			return await _categoryRepository.GetAllAsync();
		}

		public async Task<Category> GetCategoryByIdAsync(int Id)
		{
			return await _categoryRepository.FindByIdAsync(Id);
		}

		public IQueryable<Category> GetJobCategories(int JobId)
		{
			return _categoryRepository.GetJobCategories(JobId);
		}
		#endregion
	}
}
