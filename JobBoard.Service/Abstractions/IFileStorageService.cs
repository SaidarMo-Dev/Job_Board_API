using JobBoard.Data.enums;

namespace JobBoard.Service.Abstractions
{
	public interface IFileStorageService
	{

		Task<string> UploadAsync<TId>(
			Stream fileStream,
			string fileName,
			string contentType,
			FileOwnerType resource,
			TId resourceId,
			FilePathType filePathType,
			CancellationToken cancellationToken)
			where TId : notnull;


		Task DeleteAsync(string filePath);

		Task<string> CreateSignedReadUrlAsync(string filePath);
	}


}
