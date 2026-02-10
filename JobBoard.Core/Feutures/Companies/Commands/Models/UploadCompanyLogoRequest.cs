using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public class UploadCompanyLogoRequest
	{
		public required IFormFile Logo { get; set; }
	}
}
