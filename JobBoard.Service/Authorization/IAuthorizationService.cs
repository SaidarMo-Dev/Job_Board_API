using JobBoard.Data.DTOs;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Service.Authorization
{
	public interface IAuthorizationService
	{
		Task<int> AddRoleAsync(string RoleName);
		Task<bool> IsRoleExitsAsync(string RoleName);
		Task<List<Role>> GetListRolesAsync();
		Task<Role> GetRoleByIdAsync(int Id);
		Task<bool> IsRoleLinkedToUserAsync(string RoleName);
		Task<ManageUserRolesDto> GetManageUserRolesAsync(User User);
		Task<string> UpdateUserRolesAsnyc(int UserId, IEnumerable<string> Roles);
	}
}
