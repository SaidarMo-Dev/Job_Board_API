using JobBoard.Data.enums;

namespace JobBoard.Data.Entities
{
	public class FileResource
	{
		public int Id { get; set; }

		public string Bucket { get; set; } = default!;
		public string Path { get; set; } = default!;

		public FileOwnerType OwnerType { get; set; }
		public int OwnerId { get; set; }

		public FileVisibility Visibility { get; set; }

		public FileCategory? Category { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}

}
