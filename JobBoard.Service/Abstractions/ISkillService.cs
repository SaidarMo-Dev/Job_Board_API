using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface ISkillService
	{
		Task<ICollection<Skill>> GetAllAsync();
		Task<Skill> GetSkillByIdAsync(int Id);
		Task<Skill> AddNewSkillAsync(Skill entity);
		Task UpdateAsnyc(Skill entity);
		Task<bool> IsExistByNameAsync(string name);
		Task<bool> IsExistByIdAsync(int Id);
		bool IsExistById(int Id);
		Task<bool> IsExistByNameExcludeSelfAsync(int Id, string name);
		Task DeleteAsync(Skill entity);
		IQueryable<Skill> GetJobSkills(int JobId);
	}
}
