using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Admin.Command.Models
{
	public class AdminUpdateUserCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Role { get; set; }
	}
}
