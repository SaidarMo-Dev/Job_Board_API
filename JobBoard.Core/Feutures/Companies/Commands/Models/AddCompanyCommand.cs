using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public class AddCompanyCommand : IRequest<Response<int>>
	{
		public string CompanyName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string WebsiteUrl { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Fax { get; set; }

	}
}
