using JobBoard.Data.Helpers.enums;

namespace JobBoard.Data.Entities
{
	public class User
	{
		public int UserId { get; set; }
		public int PersonId { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		public RoleEnum Role { get; set; } = RoleEnum.Employer;

		public Person PersoInfo { get; set; }
		public ICollection<Application> applications { get; set; }
		public ICollection<BookMark> bookmarks { get; set; }
	}
}




