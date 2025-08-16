using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class CategoryService : ICategoryService
	{

		#region Fileds
		private readonly ICategoryRepository _categoryRepository;
		private readonly appDbContext _context;
		#endregion

		#region Constructors
		public CategoryService(ICategoryRepository categoryRepository, appDbContext context)
		{
			_categoryRepository = categoryRepository;
			_context = context;
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

		public IQueryable<Category> GetCategoriesQueryable(string? search, SortCategory? sort)
		{
			var queryable = _categoryRepository.GetTableAsNoTracking();

			if (search != null) queryable = queryable.Where(x => x.Name.Contains(search));

			if (sort != null)
			{
				switch (sort)
				{
					case SortCategory.NameAsc:
						queryable = queryable.OrderBy(x => x.Name);
						break;

					case SortCategory.NameDesc:
						queryable = queryable.OrderByDescending(x => x.Name);
						break;

					case SortCategory.NewestFirst:
						queryable = queryable.OrderByDescending(x => x.CreateDate);
						break;

					case SortCategory.OlderFirst:
						queryable = queryable.OrderBy(x => x.CreateDate);
						break;

					default:
						queryable = queryable.OrderByDescending(x => x.CreateDate);
						break;
				}


			}

			return queryable;

		}

		public async Task<List<Category>> GetPopularCategoriesAsync()
		{
			var cutOffDate = DateTime.UtcNow.AddDays(-30);

			var res = await _context.categories.Select(c => new
			{
				Id = c.CategoryId,
				c.Name,
				JobsCount = c.JobCategories.Where(x => x.jobListing.DatePosted >= cutOffDate && x.jobListing.Status == JobStatusEnum.Active).Count()
			})
				.OrderByDescending(x => x.JobsCount)
				.Take(10)
				.Select(c =>
					new Category
					{
						CategoryId = c.Id,
						Name = c.Name
					}).ToListAsync();


			return res;
		}



		#endregion
	}
}
