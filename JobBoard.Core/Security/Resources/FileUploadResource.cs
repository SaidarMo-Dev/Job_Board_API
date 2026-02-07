using JobBoard.Data.enums;

namespace JobBoard.Core.Security.Resources
{
	public class FileUploadResource
	{
		public FileOwnerType OwnerType { get; init; }
		public int OwnerId { get; init; }
	}
}
