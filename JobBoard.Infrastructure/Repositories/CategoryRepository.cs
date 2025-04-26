using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		#region Fields
		private readonly DbSet<Category> _categories;
		#endregion
		#region Constructors
		public CategoryRepository(appDbContext context) : base(context)
		{
			_categories = context.categories;
		}

		#endregion

		#region Methods
		public IQueryable<Category> GetJobCategories(int JobID)
		{
			string query = @"EXEC [Sp_GetJobCategories]
								@JobId = @JobID;";

			return _categories.FromSqlRaw(query, new SqlParameter("@JobID", JobID));

		}
		#endregion
	}
}
