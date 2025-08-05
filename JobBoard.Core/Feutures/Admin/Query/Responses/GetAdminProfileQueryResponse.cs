namespace JobBoard.Core.Feutures.Admin.Query.Responses
{
	public class GetAdminProfileQueryResponse
	{
		public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Username { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? ImagePath { get; set; }
		public string? Country { get; set; }
		public required string[] Roles { get; set; }
		public bool TwoFactorEnabled { get; set; }
	}
}
