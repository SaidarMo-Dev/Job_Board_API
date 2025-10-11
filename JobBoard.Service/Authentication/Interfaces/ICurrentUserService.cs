using System.Security.Claims;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Service.Authentication.Interfaces
{
	public interface ICurrentUserService
	{
		int GetCurrentUserId();
		Task<User> GetCurrentUserAsync();
		User GetCurrentUser();
		Task<List<string>> GetCurrentUserRoles();
		ClaimsPrincipal GetCurrentUserPrincipal();

	}
}
