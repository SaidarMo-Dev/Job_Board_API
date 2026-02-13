using System.Security.Claims;

namespace JobBoard.Data.Helpers
{
	public static class JwtClaimTypes
	{
		public const string UserId = "userId";
		public const string Email = "email";
		public const string Username = "username";
		public const string FirstName = "firstName";
		public const string LastName = "lastName";
		public const string Role = ClaimTypes.Role;
		public const string Jti = "jti";
	}

}
