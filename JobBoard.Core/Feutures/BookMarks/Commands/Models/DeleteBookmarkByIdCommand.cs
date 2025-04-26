using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.BookMarks.Commands.Models
{
	public class DeleteBookmarkByIdCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
	}
}
