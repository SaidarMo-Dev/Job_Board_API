using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;

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

		#endregion
	}
}
