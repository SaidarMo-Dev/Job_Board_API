using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Commands.Models
{
	public class AddBookMarkCommand : IRequest<Response<int>>
	{
		public int JobId { get; set; }
		public int UserId { get; set; }
		public required DateTime DateBooked { get; set; }

	}
}
