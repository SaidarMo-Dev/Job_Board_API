using JobBoard.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public class SetCompanyLogoCommand : IRequest<Response<string>>
	{
		public int CompanyId { get; set; }
		public required IFormFile Logo { get; set; }
	}
}
