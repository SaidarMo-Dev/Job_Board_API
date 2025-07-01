namespace JobBoard.Core.Feutures.BookMarks.Queries.Responses
{
	public class GetPaginatedBookmarkListQueryResponse
	{

		public int Id { get; set; }
		// user Info
		public int UserID { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		// job Info
		public int JobId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string CompanyName { get; set; }
		public required string Location { get; set; }
		public required string JobType { get; set; }
		public double MinSalary { get; set; }
		public double MaxSalary { get; set; }
		public required DateTime DatePosted { get; set; }
		public required string status { get; set; }
	}
}
