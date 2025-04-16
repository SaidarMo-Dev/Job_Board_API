using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public class UpdateCompanyCommand : IRequest<Response<int>>
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string WebsiteUrl { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Fax { get; set; }
	}
}
