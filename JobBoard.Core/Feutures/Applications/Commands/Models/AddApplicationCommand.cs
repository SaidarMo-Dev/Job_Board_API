using JobBoard.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.Applications.Commands.Models
{
	public class AddApplicationCommand : IRequest<Response<int>>
	{
		public int JobId { get; set; }
		public int UserId { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Phone { get; set; }
		public required IFormFile resume { get; set; }
		public string? CoverLetter { get; set; }
		public string? LinkedIn { get; set; }
		public string? Portfolio { get; set; }
		public required string Experience { get; set; }
		public required string Availability { get; set; }

	}
}
