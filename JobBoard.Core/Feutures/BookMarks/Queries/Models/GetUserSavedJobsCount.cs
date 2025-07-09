using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Queries.Models
{
	public class GetUserSavedJobsCount : IRequest<Response<int>>
	{
		public int UserId { get; set; }
	}
}
