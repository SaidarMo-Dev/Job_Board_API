using System.Security.Claims;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Authentication.Implementations
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;
		#region Fields
		#endregion

		#region Constructors
		public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		#endregion

		#region Handle Methods
		public int GetCurrentUserId()
		{
			var UserId = _httpContextAccessor.HttpContext?.User.Claims
							.FirstOrDefault(c => c.Type == nameof(JwtClaimModel.UserId))?.Value;

			if (UserId is null) throw new UnauthorizedAccessException();

			return int.Parse(UserId);
		}


		public async Task<User> GetCurrentUserAsync()
		{
			var userId = GetCurrentUserId();

			var user = await _userManager.FindByIdAsync(userId.ToString());

			if (user is null) throw new UnauthorizedAccessException();

			return user;
		}

		public User GetCurrentUser()
		{
			var userId = GetCurrentUserId();

			var user = _userManager.Users.Where(u => u.Id.Equals(userId)).Include(x => x.Country).FirstOrDefault();


			if (user is null) throw new UnauthorizedAccessException();

			return user;
		}

		public async Task<List<string>> GetCurrentUserRoles()
		{
			var user = GetCurrentUser();

			var userRoles = await _userManager.GetRolesAsync(user);

			return userRoles.ToList();

		}

		public ClaimsPrincipal GetCurrentUserPrincipal()
		{
			return _httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();

		}


		#endregion


	}
}
