using JobBoard.Data.Entities;
using JobBoard.Infrastructure.InfrastructureBases;

namespace JobBoard.Infrastructure.Abstractions
{
	public interface IApplicationRepository : IGenericRepository<Application>
	{
	}
}
