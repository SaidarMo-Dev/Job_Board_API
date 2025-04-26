using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
	{

		private readonly DbSet<Company> _companies;
		public CompanyRepository(appDbContext context) : base(context)
		{
			_companies = context.companies;
		}


	}
}
