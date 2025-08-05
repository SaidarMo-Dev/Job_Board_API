using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class SkillService : ISkillService
	{

		#region Fields
		private readonly ISkillRepository _skillRepository;
		#endregion

		#region Constructors
		public SkillService(ISkillRepository skillRepository)
		{
			_skillRepository = skillRepository;
		}


		#endregion

		#region Methods
		public async Task<ICollection<Skill>> GetAllAsync()
		{
			return await _skillRepository.GetAllAsync();
		}

		public async Task<Skill> GetSkillByIdAsync(int Id)
		{
			return await _skillRepository.FindByIdAsync(Id);
		}
		public async Task<Skill> AddNewSkillAsync(Skill entity)
		{
			return await _skillRepository.AddAsync(entity);
		}

		public async Task<bool> IsExistByNameAsync(string name)
		{
			var skill = await _skillRepository.GetTableAsNoTracking()
						.FirstOrDefaultAsync(x => x.Name == name);

			if (skill == null) return false;

			return true;
		}

		public async Task<bool> IsExistByIdAsync(int Id)
		{
			var skill = await _skillRepository.GetTableAsNoTracking()
						.FirstOrDefaultAsync(x => x.SkillId == Id);

			if (skill == null) return false;

			return true;
		}

		public async Task UpdateAsnyc(Skill entity)
		{
			await _skillRepository.UpdateAsync(entity);
		}

		public async Task<bool> IsExistByNameExcludeSelfAsync(int Id, string name)
		{

			var skill = await _skillRepository.GetTableAsNoTracking()
						.FirstOrDefaultAsync(x => x.Name == name && x.SkillId != Id);

			if (skill == null) return false;

			return true;
		}

		public async Task DeleteAsync(Skill skill)
		{
			await _skillRepository.DeleteAsync(skill);
		}

		public IQueryable<Skill> GetJobSkills(int JobId)
		{
			return _skillRepository.GetJobSkills(JobId);
		}

		public bool IsExistById(int Id)
		{
			var skill = _skillRepository.GetTableAsNoTracking()
						.Where(x => x.SkillId.Equals(Id))
						.FirstOrDefault();

			return skill != null;

		}

		public IQueryable<Skill> GetSkillsQueryable(string? search, SortSkill? sort)
		{
			var queryable = _skillRepository.GetTableAsNoTracking();

			if (search != null)
				queryable = queryable.Where(x => x.Name.Contains(search));

			if (sort != null && sort == SortSkill.Name)
				queryable = queryable.OrderBy(x => x.Name);
			else
				queryable = queryable.OrderBy(x => x.CreateDate);

			return queryable;
		}


		#endregion
	}
}
