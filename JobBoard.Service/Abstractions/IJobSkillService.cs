using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IJobSkillService
	{
		Task<string> AddAsync(JobSkill entity);
		Task AddRangeAsync(ICollection<JobSkill> entities);
		Task DeleteJobSkillsAsync(int JobID);
		Task<bool> IsExistById(int JobId, int SkillId);
		Task DeleteAsync(JobSkill jobSkill);
	}
}
