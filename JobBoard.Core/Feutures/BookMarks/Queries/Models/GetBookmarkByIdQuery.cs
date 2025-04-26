using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Models
{
	public class GetBookmarkByIdQuery : IRequest<Response<GetBookmarkByIdQueryResponse>>
	{
		public int Id { get; set; }
	}
}
