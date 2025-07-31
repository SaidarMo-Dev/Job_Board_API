using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Admin.Command.Models
{
	public class AdminAddUserCommand : IRequest<Response<int>>
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public required string ConfirmPassword { get; set; }
		public required string Role { get; set; }
	}
}
