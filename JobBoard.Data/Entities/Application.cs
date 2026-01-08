using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;

namespace JobBoard.Data.Entities
{
	public class Application
	{
		public int ApplicationId { get; set; }
		public int JobId { get; set; }
		public int UserId { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Phone { get; set; }

		// Old (Temporary)
		public string? ResumeUrl { get; set; }

		public int? ResumeFileId { get; set; }

		public string? CoverLetter { get; set; }
		public string? LinkedIn { get; set; }
		public string? Portfolio { get; set; }
		public required string Experience { get; set; }
		public required string Availability { get; set; }

		public required DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public ApplicationStatusEnum Status { get; set; } = ApplicationStatusEnum.Pending;
		public required DateTime LastStatusDate { get; set; }

		public required JobListing JobListing { get; set; }
		public required User UserInfo { get; set; }
		public FileResource? ResumeFile { get; set; }
	}

}


