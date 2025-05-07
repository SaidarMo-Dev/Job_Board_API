namespace JobBoard.Data.Responses
{
	public class ManageUserClaimsResponse
	{
		public int UserId { get; set; }
		public List<ClaimResponse> claimsResponse { get; set; }
	}

	public class ClaimResponse
	{
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }
		public bool HasClaim { get; set; } = false;
	}

}
