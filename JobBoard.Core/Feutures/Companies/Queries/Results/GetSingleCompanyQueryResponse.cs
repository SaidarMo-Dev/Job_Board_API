namespace JobBoard.Core.Feutures.Companies.Queries.Results
{
	public class GetSingleCompanyQueryResponse
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Industry { get; set; } = string.Empty;
		public string? Description { get; set; } = string.Empty;
		public string? WebsiteUrl { get; set; } = string.Empty;
		public required string Location { get; set; }
		public string? PhoneNumber { get; set; }
		public required string Email { get; set; }
		public string? Fax { get; set; }
	}
}
