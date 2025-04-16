using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;

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

		#endregion
	}
}
