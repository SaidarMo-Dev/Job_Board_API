using JobBoard.Core.Common.DTOs;

namespace JobBoard.Core.Feutures.Companies.Queries.Results
{
	public class GetEmployerCompanyQueryResponse
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; } = string.Empty;
		public required string Slug { get; set; }// unique

		public string Description { get; set; } = string.Empty;
		public string ShortDescription { get; set; } = default!;


		// Classification
		public string? Industry { get; set; }
		public string? CompanySize { get; set; }

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


		// Flags
		public bool IsFeatured { get; set; } = false;
		public bool IsVerified { get; set; } = false;

		// Auditing
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		// Media 
		public string? LogoUrl { get; set; }
		public string? BannerUrl { get; set; }

		public List<IndustryDto> Industries { get; set; } = new List<IndustryDto>();

	}
}
