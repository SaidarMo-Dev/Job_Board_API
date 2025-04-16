namespace JobBoard.Data.Entities
{
	public class JobCategory
	{
		public int CategoryId { get; set; }
		public int JobListingId { get; set; }

		public JobListing jobListing { get; set; }
		public Category category { get; set; }
	}
}

