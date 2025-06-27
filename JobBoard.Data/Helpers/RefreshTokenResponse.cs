namespace JobBoard.Data.Helpers
{
	public class RefreshTokenResponse
	{
		public required int UserId { get; set; }
		public required string RefreshToken { get; set; }
		public DateTime ExpirationDate { get; set; }
	}

}
