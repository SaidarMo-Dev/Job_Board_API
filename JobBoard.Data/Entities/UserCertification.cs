using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class UserCertification
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Name { get; set; } = null!;
		public string? IssuingOrganization { get; set; }

		public DateTime? IssueDate { get; set; }
		public DateTime? ExpirationDate { get; set; }

		public string? CredentialId { get; set; }
		public string? CredentialUrl { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public User User { get; set; } = null!;
	}
}
