using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class Company
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string WebsiteUrl { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;
		public string? PhoneNumber { get; set; }
		public required string Email { get; set; }
		public string? Fax { get; set; }
		public string? Industry { get; set; }
		public int? LogoFileId { get; set; }
		public int CreatedByUserId { get; set; }

		public ICollection<JobListing>? JobsListing { get; set; }
		public User CreatedByUser { get; set; }

		public FileResource? LogoFile { get; set; }

	}
}



