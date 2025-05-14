using JobBoard.Data.Entities.Identity;

namespace JobBoard.Service.Authentication.Interfaces
{
	public interface ICurrentUSerervice
	{
		int GetCurrentUserId();
		Task<User> GetCurrentUser();
	}
}
