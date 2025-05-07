using System.Security.Claims;

namespace JobBoard.Data.Helpers
{
	public static class ClaimStore
	{
		public static List<Claim> Claims => new List<Claim>
		{
			new Claim("Add", "AddJob"),
			new Claim("Edit", "EditJob"),
			new Claim("Delete", "DeleteJob"),
			new Claim("Get", "GetJob")
		};
	}
}
