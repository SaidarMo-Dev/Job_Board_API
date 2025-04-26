using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class JobSkillService : IJobSkillService
	{
		#region Fields

		private readonly IJobSkillRepository _jobSkillRepository;
		#endregion

		#region Constructors
		public JobSkillService(IJobSkillRepository jobSkillRepository)
		{
			_jobSkillRepository = jobSkillRepository;
		}


		#endregion

		#region Methods
		public async Task<string> AddAsync(JobSkill entity)
		{
			await _jobSkillRepository.AddAsync(entity);
			return "Succes";
		}

		public async Task AddRangeAsync(ICollection<JobSkill> entities)
		{
			await _jobSkillRepository.AddRangeAsync(entities);
		}

		public async Task DeleteAsync(JobSkill jobSkill)
		{
			await _jobSkillRepository.DeleteAsync(jobSkill);
		}

		public async Task DeleteJobSkillsAsync(int JobId)
		{
			var jobSkills = await _jobSkillRepository.GetTableAsNoTracking()
								.Where(x => x.JobListingId.Equals(JobId)).ToListAsync();

			await _jobSkillRepository.DeleteRangeAsync(jobSkills);
		}

		public async Task<bool> IsExistById(int JobId, int SkillId)
		{
			var result = await _jobSkillRepository.GetTableAsNoTracking()
					.Where(x => x.JobListingId.Equals(JobId) && x.SkillId.Equals(SkillId))
					.FirstOrDefaultAsync();

			return result != null;
		}
		#endregion
	}
}
