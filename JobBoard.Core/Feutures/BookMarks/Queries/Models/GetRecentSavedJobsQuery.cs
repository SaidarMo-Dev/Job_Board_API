using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Models
{
	public class GetRecentSavedJobsQuery : IRequest<Response<List<GetRecentSavedJobsQueryResponse>>>
	{
		public int Take { get; set; }
	}
}
