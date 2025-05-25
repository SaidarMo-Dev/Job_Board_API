namespace JobBoard.Core.Feutures.BookMarks.Queries.Responses
{

	public class GetUserBookmarksQueryResponse
	{
		public List<BookmarkResponse> Bookmarks { get; set; } = new();

	}

	public class BookmarkResponse
	{

		public int BookmarkId { get; set; }
		public DateTime DateBooked { get; set; }

		public BookmarkJobResponse? Job { get; set; }

	}

	public class BookmarkJobResponse
	{
		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string CompanyName { get; set; }
		public required string Location { get; set; }
		public required string JobType { get; set; }
		public string? SalaryRange { get; set; }
		public required DateTime DatePosted { get; set; }
		public required string status { get; set; }

	}
}
