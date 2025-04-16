using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IJobCategoryService
	{
		Task<bool> AddRangeAsync(ICollection<JobCategory> entities);
	}
}
