using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public class UploadCompanyBannerRequest
	{
		public IFormFile Banner { get; set; } = default!;
	};
}
