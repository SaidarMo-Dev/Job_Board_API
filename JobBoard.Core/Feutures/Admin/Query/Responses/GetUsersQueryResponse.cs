namespace JobBoard.Core.Feutures.Admin.Query.Responses
{
	public class GetUsersQueryResponse
	{
		public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string FullName => FirstName + " " + LastName;
		public required string Email { get; set; }
		public required string Username { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? ImagePath { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public string? country { get; set; }
	}
}
