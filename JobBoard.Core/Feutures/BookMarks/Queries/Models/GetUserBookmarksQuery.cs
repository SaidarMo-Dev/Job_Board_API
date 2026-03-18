using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Models
{
	public class GetUserBookmarksQuery : IRequest<PaginatedResponse<GetUserBookmarksQueryResponse>>
	{

		public int page { get; set; }
		public int pageSize { get; set; }

	}
}
