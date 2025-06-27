using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class ChangeEmailCommand : IRequest<Response<string>>
	{
		public required string CurrentEmail { get; set; }
		public required string NewEmail { get; set; }
	}
}
