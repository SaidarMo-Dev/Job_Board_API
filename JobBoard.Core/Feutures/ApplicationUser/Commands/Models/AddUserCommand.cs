
using JobBoard.Core.Bases;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class AddUserCommand : IRequest<Response<int>>
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string UserName { get; set; }
		public required string Password { get; set; }
		public required string ConfirmPassword { get; set; }
		public required string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public GendorEnum? Gendor { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
		public string? CountryName { get; set; }

	}
}
