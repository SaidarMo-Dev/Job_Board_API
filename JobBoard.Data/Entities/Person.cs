using JobBoard.Data.Helpers.enums;

namespace JobBoard.Data.Entities
{
	public class Person
	{
		public int PersonId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public GendorEnum Gendor { get; set; }
		public required DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string ImagePath { get; set; }
		public int CountryId { get; set; }

		public Country CountryInfo { get; set; }
		public User UserInfo { get; set; }
	}
}
