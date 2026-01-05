using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class UploadProfileImageRequest
	{

		public required IFormFile ProfileImage { get; set; }
	}

}
