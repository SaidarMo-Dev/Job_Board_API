using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class SkillRepository : GenericRepository<Skill>, ISkillRepository
	{

		#region Fields
		private readonly DbSet<Skill> _skills;
		#endregion
		#region Constructors
		public SkillRepository(appDbContext context) : base(context)
		{
			_skills = context.skills;
		}

		#endregion

		#region Methods

		public IQueryable<Skill> GetJobSkills(int JobID)
		{
			string query = @"Sp_GetJobSkills @JobId = @JobID";
			var result = _skills.FromSqlRaw(query, new SqlParameter("@JobID", JobID));

			return result;

		}

		#endregion


	}
}
