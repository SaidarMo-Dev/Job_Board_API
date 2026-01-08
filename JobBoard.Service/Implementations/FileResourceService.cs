using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;

namespace JobBoard.Service.Implementations
{
	public class FileResourceService : IFileResourceService
	{
		#region Fields
		private readonly IFileResourceRepository _fileResourceRepository;
		#endregion

		#region Constructors
		public FileResourceService(IFileResourceRepository fileResourceRepository)
		{
			_fileResourceRepository = fileResourceRepository;
		}

		#endregion

		#region Methods

		public async Task<FileResource> AddAsync(FileResource fileResource)
		  => await _fileResourceRepository.AddAsync(fileResource);


		public async Task<bool> DeleteAsync(FileResource fileResource)
		{
			await _fileResourceRepository.DeleteAsync(fileResource);

			return true;
		}

		public async Task<FileResource> GetByIdAsync(int Id)
			=> await _fileResourceRepository.FindByIdAsync(Id);


		public async Task<FileResource> UpdateAsync(FileResource fileResource)
		{
			await _fileResourceRepository.UpdateAsync(fileResource);

			return fileResource;
		}

		#endregion
	}
}
