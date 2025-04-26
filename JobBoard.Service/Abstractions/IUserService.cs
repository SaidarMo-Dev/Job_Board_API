using JobBoard.Data.Entities.Identity;

namespace JobBoard.Service.Abstractions
{
	public interface IUserService
	{
		Task<User> GetUserInfoByIdWithEnclude(int UserId);
		Task<string> AddNewUserAsync(User user, string Password);
		Task<string> UpdateUserAsync(User user);
		Task<bool> DeleteUsersAsync(User user);
		Task<bool> IsExistByIdAync(int UserId);
	}
}
