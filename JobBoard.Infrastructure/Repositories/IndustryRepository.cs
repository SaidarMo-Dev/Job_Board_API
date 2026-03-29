using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;

namespace JobBoard.Infrastructure.Repositories
{
	public class IndustryRepository : GenericRepository<Industry>, IIndustryRepository
	{
		#region Constructors
		public IndustryRepository(appDbContext context) : base(context)
		{

		}
		#endregion


	}
}
