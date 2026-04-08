using JobBoard.Data.enums;

namespace JobBoard.Core.Authrization.Resources
{
	public class FileUploadResource
	{
		public FileOwnerType OwnerType { get; init; }
		public int OwnerId { get; init; }
		public FileCategory Category { get; init; }
	}
}
