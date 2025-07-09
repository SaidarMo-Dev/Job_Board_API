using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Models
{
	public class GetSavedJobIdsQuery(int id) : IRequest<Response<GetSavedJobIdsQueryResponse>>
	{
		public int UserId { get; set; } = id;
	}
}
