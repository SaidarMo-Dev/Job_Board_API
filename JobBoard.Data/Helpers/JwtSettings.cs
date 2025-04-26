namespace JobBoard.Data.Helpers
{
	public class JwtSettings
	{
		public string? Issuer { get; set; }
		public string? Audience { get; set; }
		public string Secret { get; set; }
		public bool ValidateAudience { get; set; }
		public bool ValidateIssuer { get; set; }
		public bool ValidateLifeTime { get; set; }
		public bool ValidateIssuerSigninKey { get; set; }
		public int AccesTokenExpirationDuration { get; set; }
		public int RefreshTokenExpirationDuration { get; set; }

	}
}