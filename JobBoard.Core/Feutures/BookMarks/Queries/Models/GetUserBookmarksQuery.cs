using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Models
{
	public class GetUserBookmarksQuery : IRequest<Response<GetUserBookmarksQueryResponse>>
	{
		public int UserId { get; set; }

	}
}
