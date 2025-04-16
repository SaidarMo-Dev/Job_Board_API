using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.Data;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class JobSkillRepository : GenericRepository<JobSkill>, IJobSkillRepository
	{
		#region Fields
		private readonly DbSet<JobSkill> _jobSkill;
		#endregion


		#region Constructors
		public JobSkillRepository(appDbContext context) : base(context)
		{
			_jobSkill = context.jobSkills;
		}
		#endregion


		#region Methods

		#endregion

	}

}
