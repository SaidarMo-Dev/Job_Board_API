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

			if (context.User.IsInRole("Admin"))
			{
				context.Succeed(requirement);
				return Task.CompletedTask;
			}

			if (resource == null ||
				resource.Id != _currentUserService.GetCurrentUserId())
			{
				context.Fail();
				return Task.CompletedTask;
			}

			context.Succeed(requirement);
			return Task.CompletedTask;
		}
	}
}
