using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authentication.Commands.Models
{
	public class AddRecoveryContactCommand : IRequest<Response<string>>
	{
		public int UserId { get; set; }
		public required string Email { get; set; }
		public required string PhoneNumber { get; set; }
	}
}
