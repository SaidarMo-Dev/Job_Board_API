using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class ChangeUserPasswordCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public required string CurrentPassword { get; set; }
		public required string NewPassword { get; set; }
		public required string ConfirmPassword { get; set; }
	}
}
