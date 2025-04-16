using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class BookMark
	{
		public int BookMarkId { get; set; }
		public int JobId { get; set; }
		public int UserId { get; set; }
		public required DateTime dateBooked { get; set; }

		public User userInfo { get; set; }
		public JobListing jobListing { get; set; }

	}
}


