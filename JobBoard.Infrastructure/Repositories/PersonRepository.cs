using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.Data;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class PersonRepository : GenericRepository<Person>, IPersonRepository
	{
		#region fields
		private readonly DbSet<Person> _people;
		#endregion

		#region constructors
		public PersonRepository(appDbContext context) : base(context)
		{
			_people = context.people;
		}
		#endregion

		#region methods
		#endregion

	}
}
