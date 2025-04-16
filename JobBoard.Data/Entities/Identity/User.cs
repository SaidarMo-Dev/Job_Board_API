using JobBoard.Data.Helpers.enums;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Data.Entities.Identity
{
	public class User : IdentityUser<int>
	{
		public User()
		{
			applications = new HashSet<Application>();
			bookmarks = new HashSet<BookMark>();
			jobs = new HashSet<JobListing>();
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => FirstName + " " + LastName;
		public GendorEnum Gendor { get; set; }
		public required DateTime DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
		public int CountryId { get; set; }

		public Country Country { get; set; }
		public ICollection<Application> applications { get; set; }
		public ICollection<BookMark> bookmarks { get; set; }
		public ICollection<JobListing> jobs { get; set; }

	}
}




