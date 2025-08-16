namespace JobBoard.Core.Common.DTOs
{
	public class CompanyDto
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

	}
}
