using JobBoard.Data.enums;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Data.Entities.Identity
{
	public class User : IdentityUser<int>
	{
		public User()
		{
			applications = new HashSet<Application>();
			bookmarks = new HashSet<Bookmark>();
			CreatedJobs = new HashSet<JobListing>();
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => FirstName + " " + LastName;
		public GendorEnum? Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? CountryId { get; set; }
		public string? Code { get; set; }
		public string? RecoveryEmail { get; set; }
		public string? RecoveryPhone { get; set; }

		public string? Jti { get; set; }
		public bool? JtiExp { get; set; }

		public Country? Country { get; set; }
		public ICollection<Application> applications { get; set; }
		public ICollection<Bookmark> bookmarks { get; set; }
		public ICollection<JobListing> CreatedJobs { get; set; }
		public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
		public ICollection<Company> CreatedCompanies { get; set; }
	}
}




