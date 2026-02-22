using JobBoard.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class SetUserProfileImageCommand : IRequest<Response<string>>
	{
		public required IFormFile ProfileImage { get; set; }

	}
}
