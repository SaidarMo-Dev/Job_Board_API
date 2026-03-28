namespace JobBoard.Core.Feutures.Companies.Queries.Results
{
	public class GetListCompaniesQueryesponse
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; } = string.Empty;
		public required string Slug { get; set; }

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

		public string Location { get; set; } = default!;

		// Media
		public string? LogoUrl { get; set; }
		public string? BannerUrl { get; set; }

		public string? PhoneNumber { get; set; }
		public required string Email { get; set; }
		public string? Fax { get; set; }


		// Flags
		public bool IsFeatured { get; set; } = false;
		public bool IsVerified { get; set; } = false;

		public DateTime CreatedAt { get; set; }
		public int TotalJobs { get; set; }
		public string CreatedByUser { get; set; } = default!;

	}

}
