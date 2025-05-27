using JobBoard.Core.Security.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Security.Handlers
{
	public class UserApplicationsHandler : AuthorizationHandler<UserApplicationsRequirement, Application>
	{
		private readonly ICurrentUserService _currentUserService;

		public UserApplicationsHandler(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserApplicationsRequirement requirement, Application resource)
		{
			var userId = _currentUserService.GetCurrentUserId();

			if (resource.UserId.Equals(userId))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
