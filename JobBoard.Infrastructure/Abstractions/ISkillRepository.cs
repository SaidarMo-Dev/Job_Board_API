using JobBoard.Data.Entities;
using JobBoard.Infrastructure.InfrastructureBases;

namespace JobBoard.Infrastructure.Abstractions
{
	public interface ISkillRepository : IGenericRepository<Skill>
	{
		IQueryable<Skill> GetJobSkills(int JobId);
	}
}
