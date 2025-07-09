using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Models
{
	public class GetUserBookmarksQuery : IRequest<PaginatedResponse<List<GetUserBookmarksQueryResponse>>>
	{
		public int UserId { get; set; }
		public int page { get; set; }
		public int pageSize { get; set; }

	}
}
