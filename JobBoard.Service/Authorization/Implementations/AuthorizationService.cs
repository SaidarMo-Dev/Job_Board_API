using System.Security.Claims;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Data.Requests;
using JobBoard.Data.Responses;
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
		public async Task<ManageUserRolesDto> ManageUserRolesAsync(User user)
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

				if (!result.Succeeded) return "FailedToRemoveUserRoles";

				var AddRolesResult = await _userManager.AddToRolesAsync(user, Roles);

				if (!AddRolesResult.Succeeded) return "FailedToAddUserRoles";

				await trans.CommitAsync();
				return "Success";
			}
			catch
			{
				await trans.RollbackAsync();
				return "FailedToAddUserRoles";
			}

		}

		public async Task<ManageUserClaimsResponse> ManageUserClaimsAsync(User User)
		{
			var manageUserClaims = new ManageUserClaimsResponse();
			manageUserClaims.UserId = User.Id;

			manageUserClaims.claimsResponse = new List<ClaimResponse>();

			var userClaims = await _userManager.GetClaimsAsync(User);

			foreach (var claim in ClaimStore.Claims)
			{
				var claimResponse = new ClaimResponse() { ClaimType = claim.Type, ClaimValue = claim.Value };


				if (userClaims.Any(x => x.Value == claim.Value))
				{
					claimResponse.HasClaim = true;
				}

				manageUserClaims.claimsResponse.Add(claimResponse);
			}

			return manageUserClaims;
		}

		public async Task<string> UpdateUserClaimsAsnyc(UpdateUserClaimRequest request)
		{
			var trans = await _context.Database.BeginTransactionAsync();

			try
			{
				var user = await _userManager.FindByIdAsync(request.UserId.ToString());
				if (user is null) return "NotFound";

				var userClaims = await _userManager.GetClaimsAsync(user);
				// delete all claims
				var result = await _userManager.RemoveClaimsAsync(user, userClaims);
				if (!result.Succeeded) return "FailedToDeleteUserClaims";

				var newClaims = request.claimsResponse.Where(claim => claim.HasClaim)
							.Select(claim => new Claim(claim.ClaimType, claim.ClaimValue));

				var addResult = await _userManager.AddClaimsAsync(user, newClaims);


				if (!addResult.Succeeded) return "FailedToAddClaims";

				await trans.CommitAsync();

				return "Success";

			}
			catch
			{
				await trans.RollbackAsync();
				return "ErrorUpdateClaims";
			}

		}


		#endregion
	}
}
