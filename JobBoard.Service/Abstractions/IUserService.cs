using JobBoard.Data.Entities.Identity;

namespace JobBoard.Service.Abstractions
{
	public interface IUserService
	{
		Task<User> GetUserInfoByIdWithEnclude(int UserId);
		Task<string> AddNewUser(User user, string Password);
		Task<string> UpdateUser(User user);
	}
}
