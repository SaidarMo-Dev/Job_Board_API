using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers.enums;

namespace JobBoard.Data.Entities
{
	public class Application
	{
		public int ApplicationId { get; set; }
		public int JobListingId { get; set; }
		public int UserId { get; set; }
		public required DateTime ApplicationDate { get; set; }
		public ApplicationStatusEnum status { get; set; }

		public JobListing jobListing { get; set; }
		public User userInfo { get; set; }
	}

}


