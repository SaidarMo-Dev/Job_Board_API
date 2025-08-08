using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public class UpdateCompanyCommand : IRequest<Response<int>>
	{
		public int CompanyId { get; set; }
		public string? CompanyName { get; set; }
		public string? Description { get; set; }
		public string? WebsiteUrl { get; set; }
		public string? Location { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
		public string? Fax { get; set; }
		public string? Industry { get; set; }

	}
}
