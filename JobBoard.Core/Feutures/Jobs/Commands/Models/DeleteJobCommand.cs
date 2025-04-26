using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Commands.Models
{
	public class DeleteJobCommand(int Id) : IRequest<Response<string>>
	{
		public int Id { get; set; } = Id;
	}
}
