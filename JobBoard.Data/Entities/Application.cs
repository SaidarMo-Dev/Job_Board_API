using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;

namespace JobBoard.Data.Entities
{
	public class Application
	{
		public int ApplicationId { get; set; }
		public int JobListingId { get; set; }
		public int UserId { get; set; }
		public string? Description { get; set; }
		public required DateTime CreatedOn { get; set; }
		public ApplicationStatusEnum status { get; set; }
		public required DateTime LastStatusDate { get; set; }

		public JobListing JobListing { get; set; }
		public User UserInfo { get; set; }
	}

}


