using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Requests;
using JobBoard.Data.Responses;

namespace JobBoard.Service.Authorization
{
	public interface IAuthorizationService
	{
		Task<int> AddRoleAsync(string RoleName);
		Task<bool> IsRoleExitsAsync(string RoleName);
		Task<List<Role>> GetListRolesAsync();
		Task<Role> GetRoleByIdAsync(int Id);
		Task<bool> IsRoleLinkedToUserAsync(string RoleName);
		Task<ManageUserRolesDto> ManageUserRolesAsync(User User);
		Task<string> UpdateUserRolesAsnyc(int UserId, IEnumerable<string> Roles);
		Task<ManageUserClaimsResponse> ManageUserClaimsAsync(User User);
		Task<string> UpdateUserClaimsAsnyc(UpdateUserClaimRequest request);

	}
}
