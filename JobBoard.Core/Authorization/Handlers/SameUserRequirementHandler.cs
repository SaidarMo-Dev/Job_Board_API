using JobBoard.Core.Authrization.Requirements;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Core.Authrization.Handlers
{
	public class SameUserRequirementHandler : AuthorizationHandler<SameUserRequirement, User>
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly UserManager<User> _userManager;

		public SameUserRequirementHandler(ICurrentUserService currentUserService, UserManager<User> userManager)
		{
			_currentUserService = currentUserService;
			_userManager = userManager;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
														SameUserRequirement requirement,
														User resource)
		{


			var id = _currentUserService.GetCurrentUserId();

			if (context.User.IsInRole("Admin"))
			{
				context.Succeed(requirement);
				return Task.CompletedTask;

			}


			if (resource.Id == id)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
