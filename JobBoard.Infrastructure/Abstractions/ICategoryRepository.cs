using JobBoard.Data.Entities;
using JobBoard.Infrastructure.InfrastructureBases;

namespace JobBoard.Infrastructure.Abstractions
{
	public interface ICategoryRepository : IGenericRepository<Category>
	{
		IQueryable<Category> GetJobCategories(int JobID);
	}

}
