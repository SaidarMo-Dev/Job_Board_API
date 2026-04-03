namespace JobBoard.Core.Common.DTOs
{
	public class CertificationDto
	{
		public string Name { get; set; } = null!;
		public string? IssuingOrganization { get; set; }
		public string? CredentialId { get; set; }
		public string? CredentialUrl { get; set; }
		public DateTime? IssueDate { get; set; }
	}

}
