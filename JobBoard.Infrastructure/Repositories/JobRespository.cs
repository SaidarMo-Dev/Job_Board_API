using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class JobRepository : GenericRepository<JobListing>, IJobRepository
	{
		#region Fields
		private readonly DbSet<JobListing> _jobs;
		#endregion

		#region nstructors
		public JobRepository(appDbContext context) : base(context)
		{
			_jobs = context.jobs;
		}


		#endregion

		#region Methods


		#endregion
	}
}
