namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Responses
{
	public class GetCurrentUserQueryResponse
	{
		public int Id { get; set; }
		public required string Email { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string? Gender { get; set; }
		public string? PhoneNumber { get; set; }
		public required string DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
		public string? CountryName { get; set; }
		public string? RecoveryEmail { get; set; }
		public string? RecoveryPhone { get; set; }
		public string? ProfileImageUrl { get; set; }
	}
}
