namespace JobBoard.Core.Feutures.BookMarks.Queries.Responses
{
	public class GetRecentSavedJobsQueryResponse
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required string Company { get; set; }
		public DateOnly SavedAt { get; set; }

	}
}
