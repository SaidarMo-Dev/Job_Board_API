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


		Task DeleteAsync(FileOwnerType ownerType, string filePath);

		Task<string> CreateSignedReadUrlAsync(string bucket, string filePath);
		string GetPublicUrl(string bucket, string filePath);
		string GetBucket(FileOwnerType ownerType);
	}


}
