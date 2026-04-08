using JobBoard.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public record UploadCompanyBannerCommand(int CompanyId, IFormFile File) : IRequest<Response<string>>;
}
