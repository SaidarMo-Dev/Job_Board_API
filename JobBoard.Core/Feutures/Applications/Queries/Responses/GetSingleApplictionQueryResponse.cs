namespace JobBoard.Core.Feutures.Applications.Queries.Responses
{
	public class GetSingleApplictionQueryResponse
	{
		public int ApplicationId { get; set; }
		public string? Description { get; set; }
		public required DateTime CreatedOn { get; set; }
		public required string Status { get; set; }
		public required DateTime LastStatusDate { get; set; }
		public required UserResponse UserResponse { get; set; }
		public required JobResponse JobResponse { get; set; }


	}

	public class UserResponse
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
	}

	public class JobResponse
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string Location { get; set; }
		public required string CompanyName { get; set; }
		public required string JobType { get; set; }
		public string? SalaryRange { get; set; }
		public required DateTime DatePosted { get; set; }
		public required string status { get; set; }

	}
}
