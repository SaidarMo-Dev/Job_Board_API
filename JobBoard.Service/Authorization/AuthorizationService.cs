
using JobBoard.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Authorization
{
	public class AuthorizationService : IAuthorizationService
	{
		#region Fields
		private readonly RoleManager<Role> _roleManager;

		#endregion

		#region Constructors
		public AuthorizationService(RoleManager<Role> roleManager)
		{
			_roleManager = roleManager;
		}

		#endregion


		#region Methods
		public async Task<int> AddRoleAsync(string RoleName)
		{
			var role = new Role();
			role.Name = RoleName;

			var result = await _roleManager.CreateAsync(role);

			if (!result.Succeeded) return -1;
			return role.Id;

		}


		public async Task<bool> IsRoleExitsAsync(string RoleName)
		{
			return await _roleManager.RoleExistsAsync(RoleName);
		}
		public async Task<List<Role>> GetListRolesAsync()
		{
			return await _roleManager.Roles.ToListAsync();
		}

		public async Task<Role> GetRoleByIdAsync(int Id)
		{
			return await _roleManager.FindByIdAsync(Id.ToString());
		}


		#endregion
	}
}
