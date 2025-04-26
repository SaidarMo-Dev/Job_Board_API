using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure.InfrastructureBases;

namespace JobBoard.Infrastructure.Abstractions
{
	public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshToken>
	{
	}
}
