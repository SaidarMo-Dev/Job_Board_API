
using JobBoard.Core.Bases;
using JobBoard.Data.Helpers.enums;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class AddUserCommand : IRequest<Response<int>>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public GendorEnum Gendor { get; set; }
		public required DateTime DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
		public string CountryName { get; set; }

	}
}
