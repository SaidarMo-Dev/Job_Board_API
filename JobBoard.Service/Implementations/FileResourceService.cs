using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

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
		{
			//// Try to find existing file by Path
			//var existing = await _fileResourceRepository
			//	.GetTableAsNoTracking()
			//	.FirstOrDefaultAsync(fr => fr.Path == fileResource.Path);

			//if (existing is not null)
			//{
			//	// Update properties of existing record if needed
			//	existing.Bucket = fileResource.Bucket;
			//	existing.Visibility = fileResource.Visibility;
			//	existing.OwnerId = fileResource.OwnerId;
			//	existing.OwnerType = fileResource.OwnerType;
			//	existing.Path = fileResource.Path;

			//	// Save changes directly
			//	await _fileResourceRepository.SaveChangesAsync();
			//	return existing;
			//}

			// Add new record if it doesn't exist
			return await _fileResourceRepository.AddAsync(fileResource);
		}


		public async Task<bool> DeleteAsync(FileResource fileResource)
		{
			await _fileResourceRepository.DeleteAsync(fileResource);

			return true;
		}

		public async Task<FileResource> GetByIdAsync(int Id)
			=> await _fileResourceRepository.FindByIdAsync(Id);

		public async Task<FileResource> GetByOwnerAsync(FileOwnerType ownerType, int ownerId)
		{
			return await _fileResourceRepository.GetTableAsNoTracking()
				.FirstOrDefaultAsync(fr => fr.OwnerType == ownerType && fr.OwnerId == ownerId);


		}

		public async Task<FileResource> UpdateAsync(FileResource fileResource)
		{
			await _fileResourceRepository.UpdateAsync(fileResource);

			return fileResource;
		}

		#endregion
	}
}
