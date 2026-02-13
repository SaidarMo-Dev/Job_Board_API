using JobBoard.Core.Authrization.Requirements;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Core.Authrization.Handlers
{
	public class SameUserHandler : AuthorizationHandler<SameUserRequirement, User>
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly UserManager<User> _userManager;

		public SameUserHandler(ICurrentUserService currentUserService, UserManager<User> userManager)
		{
			_currentUserService = currentUserService;
			_userManager = userManager;
		}

		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
														SameUserRequirement requirement,
														User resource)
		{


			var user = _currentUserService.GetCurrentUser();


			if (await _userManager.IsInRoleAsync(user, "Admin")) context.Succeed(requirement);


			if (resource.Id == user.Id)
			{
				context.Succeed(requirement);
			}

		}
	}
}
