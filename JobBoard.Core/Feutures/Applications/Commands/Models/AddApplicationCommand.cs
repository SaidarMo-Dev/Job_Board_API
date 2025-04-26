using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Commands.Models
{
	public class AddApplicationCommand : IRequest<Response<int>>
	{
		public int JobId { get; set; }
		public int UserId { get; set; }
		public string? Description { get; set; }
	}
}
