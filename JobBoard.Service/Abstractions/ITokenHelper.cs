using System.Security.Claims;

namespace JobBoard.Service.Abstractions
{
	public interface ITokenHelper
	{
		public ClaimsPrincipal? ValidateToken(string token);
	}
}
