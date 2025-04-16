using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.Data;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class JobCategoryRepository : GenericRepository<JobCategory>, IJobCategoryRepository
	{

		#region Fields
		private DbSet<JobCategory> _jobCatgories;
		#endregion

		#region Constructors
		public JobCategoryRepository(appDbContext context) : base(context)
		{
			_jobCatgories = context.jobCategories;
		}
		#endregion

		#region Fields
		#endregion
	}
}
