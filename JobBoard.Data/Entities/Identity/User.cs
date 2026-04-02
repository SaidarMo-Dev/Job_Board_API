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

		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string FullName => FirstName + " " + LastName;
		public GendorEnum? Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
		public int? ProfileImageFileId { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? CountryId { get; set; }
		public string? Code { get; set; }
		public string? RecoveryEmail { get; set; }
		public string? RecoveryPhone { get; set; }

		public string? Jti { get; set; }
		public bool? JtiExp { get; set; }

		// Navigation
		public Country? Country { get; set; }
		public ICollection<Application> applications { get; set; }
		public ICollection<Bookmark> bookmarks { get; set; }
		public ICollection<JobListing> CreatedJobs { get; set; }
		public ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new List<UserRefreshToken>();
		public ICollection<Company> CreatedCompanies { get; set; } = new List<Company>();
		public FileResource? ProfileImageFile { get; set; }

		public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
		public ICollection<UserExperience> Experiences { get; set; } = new List<UserExperience>();
		public ICollection<UserEducation> Educations { get; set; } = new List<UserEducation>();
		public ICollection<UserCertification> Certifications { get; set; } = new List<UserCertification>();
		public ICollection<UserLanguage> Languages { get; set; } = new List<UserLanguage>();
		public UserJobPreference? JobPreference { get; set; }
		public UserProfileStats? ProfileStats { get; set; }
	}
}




