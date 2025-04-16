using JobBoard.Core.Bases;
using JobBoard.Data.Helpers.enums;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class UpdateUserCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public GendorEnum Gendor { get; set; }
		public required DateTime DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
	}
}
