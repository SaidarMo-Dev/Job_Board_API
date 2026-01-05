using JobBoard.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class SetUserProfileImageCommand : IRequest<Response<string>>
	{
		public int UserId { get; set; }
		public required IFormFile ProfileImage { get; set; }

	}
}
