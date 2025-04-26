using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IJobService
	{
		Task<string> AddNewJobAsync(JobListing entity);
		Task<JobListing> GetJobByIdWithEncludeAsync(int Id);
		Task<JobListing> GetJobByIdWithEncludeSkillsAndCategoriesAsync(int Id);
		Task<JobListing> GetJobByIdAsync(int Id);
		IQueryable<JobListing> GetJobsQueryable();
		Task<List<Skill>> GetJobSkillsAsync(int JobId);
		Task<List<Category>> GetJobCategoriesAsync(int JobId);
		Task<JobListing> UpdateAsync(JobListing job);
		Task<bool> DeleteJobAsync(JobListing job);
		Task<bool> IsExistByIdAsync(int JobId);


	}
}
