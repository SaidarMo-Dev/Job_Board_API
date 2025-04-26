using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class JobCategoryService : IJobCategoryService
	{

		#region Fields
		private readonly IJobCategoryRepository _jobCategoryRepository;
		#endregion


		#region Constructors
		public JobCategoryService(IJobCategoryRepository jobCategoryRepository)
		{
			_jobCategoryRepository = jobCategoryRepository;
		}

		#endregion


		#region Methods

		public async Task<bool> AddRangeAsync(ICollection<JobCategory> entities)
		{
			await _jobCategoryRepository.AddRangeAsync(entities);
			return true;
		}

		public async Task DeleteAsync(JobCategory jobCategory)
		{
			await _jobCategoryRepository.DeleteAsync(jobCategory);
		}

		public async Task<bool> IsExistById(int jobId, int categoryId)
		{
			var result = await _jobCategoryRepository.GetTableAsNoTracking()
				.Where(x => x.JobListingId.Equals(jobId) && x.CategoryId.Equals(categoryId))
				.FirstOrDefaultAsync();

			return result != null;
		}
		public async Task AddAsync(JobCategory jobCategory)
		{
			await _jobCategoryRepository.AddAsync(jobCategory);

		}

		#endregion
	}
}
