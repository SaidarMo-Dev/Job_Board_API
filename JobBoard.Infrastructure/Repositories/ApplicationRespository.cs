using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
	{
		#region Fields
		private DbSet<Application> _applications;
		#endregion

		#region Constructors
		public ApplicationRepository(appDbContext context) : base(context)
		{
			_applications = context.applications;
		}

		#endregion

		#region Methods
		#endregion

	}
}
