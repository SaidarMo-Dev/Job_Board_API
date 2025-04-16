namespace JobBoard.Core.Feutures.Companies.Queries.Results
{
	public class GetPaginatedListCompaniesQueryResponse
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string WebsiteUrl { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Email { get; set; }
		public string? Fax { get; set; }

		public GetPaginatedListCompaniesQueryResponse(int CompanyId, string CompanyName, string Description,
													string WebsiteUrl, string Location, string PhoneNumber, string Email, string Fax)
		{
			this.CompanyId = CompanyId;
			this.CompanyName = CompanyName;
			this.Description = Description;
			this.WebsiteUrl = WebsiteUrl;
			this.Location = Location;
			this.PhoneNumber = PhoneNumber;
			this.Email = Email;
			this.Fax = Fax;

		}
	}
}
