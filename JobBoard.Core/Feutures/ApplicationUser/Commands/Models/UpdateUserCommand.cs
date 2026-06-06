using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class UpdateUserCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string? Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
		public string? CountryName { get; set; }
		public string? PhoneNumber { get; set; }

	}
}
