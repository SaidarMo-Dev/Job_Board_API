namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Responses
{
	public class GetUseBookmarksQueryResponse
	{
		public int UserId { get; set; }
		public required string Username { get; set; }
		public BookmarkResponse? BookmarkResponse { get; set; }

	}

	public class BookmarkResponse
	{

		public int BookmarkId { get; set; }
		public DateTime DateBooked { get; set; }

		// job Info
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
