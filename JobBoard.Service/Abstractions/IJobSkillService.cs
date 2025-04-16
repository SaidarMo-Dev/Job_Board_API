using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IJobSkillService
	{
		Task<string> AddAsync(JobSkill entity);
		Task AddRangeAsync(ICollection<JobSkill> entities);

	}
}
