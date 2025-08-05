namespace JobBoard.Data.Helpers
{
	public static class JwtClaimModel
	{

		public static int UserId { get; set; }
		public static string? Username { get; set; }
		public static string? Email { get; set; }
		public static string? FirstName { get; set; }
		public static string? LastName { get; set; }
		public static string? role { get; set; }

	}
}
