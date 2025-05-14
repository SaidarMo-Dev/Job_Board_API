using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Service.Authentication.Implementations
{
	public class CurrentUserService : ICurrentUSerervice
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
			var UserId = _httpContextAccessor.HttpContext.User.Claims
							.FirstOrDefault(c => c.Type == nameof(JwtClaimModel.UserId))?.Value;

			if (UserId is null) throw new UnauthorizedAccessException();

			return int.Parse(UserId);
		}


		public async Task<User> GetCurrentUser()
		{
			var userId = GetCurrentUserId();

			var user = await _userManager.FindByIdAsync(userId.ToString());

			if (user is null) throw new UnauthorizedAccessException();

			return user;
		}



		#endregion


	}
}
