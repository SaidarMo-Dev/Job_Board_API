using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Commands.Models
{
	public class DeleteApplicationCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
	}
}
