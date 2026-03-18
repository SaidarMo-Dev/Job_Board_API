using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class Company
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; } = string.Empty;
		public required string Slug { get; set; }// unique

		public string Description { get; set; } = string.Empty;
		public string ShortDescription { get; set; } = default!;


		// Classification
		public string? Industry { get; set; }
		public string? CompanySize { get; set; } // e.g. 1-10, 11-50

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
		public int? LogoFileId { get; set; }
		public string? BannerUrl { get; set; }

		public string? PhoneNumber { get; set; }
		public required string Email { get; set; }
		public string? Fax { get; set; }


		// Flags
		public bool IsFeatured { get; set; } = false;
		public bool IsVerified { get; set; } = false;

		// Ownership
		public int CreatedByUserId { get; set; }

		// Navigations
		public ICollection<JobListing>? JobListings { get; set; }
		public User CreatedByUser { get; set; } = default!;

		public FileResource? LogoFile { get; set; }

		// Auditing
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }

	}



}



