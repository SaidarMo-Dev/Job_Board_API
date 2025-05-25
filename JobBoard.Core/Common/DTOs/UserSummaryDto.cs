namespace JobBoard.Core.Common.DTOs
{
	public class UserSummaryDto
	{

		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string FullName => FirstName + " " + LastName;
		public required string Gender { get; set; }
		public required DateTime DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? ImagePath { get; set; }
		public required string CountryName { get; set; }

	}

}
