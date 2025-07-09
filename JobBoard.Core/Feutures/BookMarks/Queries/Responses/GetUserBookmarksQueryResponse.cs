using JobBoard.Core.Common.DTOs;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Responses
{

	public class GetUserBookmarksQueryResponse
	{
		public int BookmarkId { get; set; }
		public DateTime DateBooked { get; set; }

		public JobResponseDto? Job { get; set; }
	}

}
