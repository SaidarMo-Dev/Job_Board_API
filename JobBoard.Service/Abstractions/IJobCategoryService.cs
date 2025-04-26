using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IJobCategoryService
	{
		Task<bool> AddRangeAsync(ICollection<JobCategory> entities);
		Task AddAsync(JobCategory jobCategory);
		Task DeleteAsync(JobCategory jobCategory);
		Task<bool> IsExistById(int jobId, int categoryId);
	}
}
