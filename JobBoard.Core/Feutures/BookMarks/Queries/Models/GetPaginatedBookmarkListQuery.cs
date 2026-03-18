using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Models
{
	public class GetPaginatedBookmarkListQuery : IRequest<PaginatedResponse<GetPaginatedBookmarkListQueryResponse>>
	{
		public int Page { get; set; }
		public int Size { get; set; }

	}
}
