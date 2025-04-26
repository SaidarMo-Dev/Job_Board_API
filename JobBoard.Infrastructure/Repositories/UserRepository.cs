using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;

namespace JobBoard.Infrastructure.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{


		#region Fields

		#endregion

		#region Constructors
		public UserRepository(appDbContext appDbContext) : base(appDbContext)
		{

		}

		#endregion

	}
}
