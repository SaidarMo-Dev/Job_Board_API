using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;

namespace JobBoard.Infrastructure.Repositories
{
	public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository
	{

		#region Construcotrs
		public UserRefreshTokenRepository(appDbContext context) : base(context)
		{
		}
		#endregion

		#region Methods 

		#endregion

	}
}
