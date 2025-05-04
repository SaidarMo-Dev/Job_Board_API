
using JobBoard.Data.DTOs;
using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure.context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Authorization
{
	public class AuthorizationService : IAuthorizationService
	{
		#region Fields
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;
		private readonly appDbContext _context;

		#endregion

		#region Constructors
		public AuthorizationService(RoleManager<Role> roleManager,
									UserManager<User> userManager,
									appDbContext context)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_context = context;
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

		public async Task<bool> IsRoleLinkedToUserAsync(string RoleName)
		{
			var result = await _userManager.GetUsersInRoleAsync(RoleName);

			return result != null;
		}
		public async Task<ManageUserRolesDto> GetManageUserRolesAsync(User user)
		{
			var result = new ManageUserRolesDto();
			result.Roles = new List<RoleResponse>();

			result.UserId = user.Id;

			var userRoles = await _userManager.GetRolesAsync(user);

			var roles = await _roleManager.Roles.ToListAsync();

			foreach (var role in roles)
			{
				var roleResponse = new RoleResponse();

				roleResponse.Id = role.Id;
				roleResponse.Name = role.Name;

				if (userRoles.Contains(role.Name))
				{
					roleResponse.HasRodle = true;
				}

				result.Roles.Add(roleResponse);
			}

			return result;
		}

		public async Task<string> UpdateUserRolesAsnyc(int UserId, IEnumerable<string> Roles)
		{
			var trans = await _context.Database.BeginTransactionAsync();
			try
			{
				var user = await _userManager.FindByIdAsync(UserId.ToString());
				if (user is null) return "UserNotFound";

				var userRoles = await _userManager.GetRolesAsync(user);

				var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

				if (!result.Succeeded) return "FaildToRemoveUserRoles";

				var AddRolesResult = await _userManager.AddToRolesAsync(user, Roles);

				if (!AddRolesResult.Succeeded) return "FaildToAddUserRoles";

				await trans.CommitAsync();
				return "Success";
			}
			catch
			{
				await trans.RollbackAsync();
				return "Failed";
			}

		}


		#endregion
	}
}
