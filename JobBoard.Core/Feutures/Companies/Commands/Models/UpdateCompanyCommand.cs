using JobBoard.Core.Bases;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public class UpdateCompanyCommand : IRequest<Response<int>>
	{

		public int CompanyId { get; set; } = default!;
		public string CompanyName { get; set; } = default!;
		public string Slug { get; set; } = default!; // unique

		public string Description { get; set; } = string.Empty;
		public string ShortDescription { get; set; } = default!;


		public CompanySize? CompanySize { get; set; }

		public int? FoundedYear { get; set; }

		// Links
		public string WebsiteUrl { get; set; } = default!;
		public string? LinkedInUrl { get; set; }
		public string? TwitterUrl { get; set; }

		// Location
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? Address { get; set; }

		public string? PhoneNumber { get; set; }
		public required string Email { get; set; }
		public string? Fax { get; set; }

		public List<int> IndustryIds { get; set; } = new();

	}
}
