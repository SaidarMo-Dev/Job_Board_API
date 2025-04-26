using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class BookMarkRepository : GenericRepository<Bookmark>, IBookMarkRepository
	{

		#region Fields
		private readonly DbSet<Bookmark> _bookMarks;
		#endregion

		#region Contsructors
		public BookMarkRepository(appDbContext context) : base(context)
		{
			_bookMarks = context.bookMarks;
		}
		#endregion

		#region Methods
		#endregion
	}
}
