namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Responses
{
	public class GetPaginatedListUsersQueryResponse
	{
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gendor { get; set; }
		public required string DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? PhoneNumber { get; set; }
		public string? ImagePath { get; set; }
		public string CountryName { get; set; }
	}
}
