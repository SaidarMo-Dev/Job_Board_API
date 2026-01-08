using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
	public class FileResourceRepository : GenericRepository<FileResource>, IFileResourceRepository
	{
		#region Fields
		public readonly DbSet<FileResource> _fileResources;
		#endregion

		#region Constructors
		public FileResourceRepository(appDbContext context) : base(context)
		{
			_fileResources = context.fileResources;
		}

		#endregion


		#region Methods
		#endregion
	}
}
