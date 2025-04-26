namespace JobBoard.Data.Helpers
{
	public class AuthResponse
	{
		public required string AccessToken { get; set; }
		public RefreshTokenResponse? RefreshToken { get; set; }
	}

}
