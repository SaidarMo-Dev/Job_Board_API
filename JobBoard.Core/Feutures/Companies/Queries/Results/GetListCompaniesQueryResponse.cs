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
		public string? CompanySize { get; set; }

		// Location
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? Address { get; set; }

		public string Location { get; set; } = default!;

		// Media
		public string? LogoUrl { get; set; }


		// Flags
		public bool IsFeatured { get; set; } = false;
		public bool IsVerified { get; set; } = false;

		public DateTime CreatedAt { get; set; }
		public int TotalJobs { get; set; }
		public int TotalOpenJobs { get; set; }


	}

}
