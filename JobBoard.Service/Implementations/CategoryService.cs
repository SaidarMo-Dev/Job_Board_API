using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

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

		public async Task<Category> FindById(int Id)
		{
			return await _categoryRepository.FindByIdAsync(Id);
		}

		public IQueryable<Category> GetJobCategories(int JobId)
		{
			return _categoryRepository.GetJobCategories(JobId);
		}

		public bool IsExistById(int Id)
		{
			var category = _categoryRepository.GetTableAsNoTracking()
							.Where(x => x.CategoryId.Equals(Id))
							.FirstOrDefault();

			return category != null;
		}

		public async Task<bool> IsExistByIdAsync(int Id)
		{
			var category = await _categoryRepository.GetTableAsNoTracking()
							.Where(x => x.CategoryId.Equals(Id))
							.FirstOrDefaultAsync();

			return category != null;

		}

		public async Task<int> AddAsync(Category category)
		{
			await _categoryRepository.AddAsync(category);

			return category.CategoryId;

		}

		public async Task<bool> IsNameExistAsync(string Name)
		{
			var result = await _categoryRepository.GetTableAsNoTracking()
									.Where(x => x.Name.Equals(Name))
									.FirstOrDefaultAsync();

			return result != null;

		}
		public async Task<bool> IsNameExistExcludeSelfAsync(int Id, string Name)
		{
			var result = await _categoryRepository.GetTableAsNoTracking()
									.Where(x => x.Name.Equals(Name) && x.CategoryId != Id)
									.FirstOrDefaultAsync();

			return result != null;

		}

		public async Task<string> UpdateAsync(Category category)
		{
			await _categoryRepository.UpdateAsync(category);
			return "Success";
		}

		public async Task<string> DeleteAsync(Category category)
		{
			await _categoryRepository.DeleteAsync(category);

			return "Success";
		}



		#endregion
	}
}
