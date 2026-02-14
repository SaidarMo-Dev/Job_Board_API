using JobBoard.Core.Authrization.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Authrization.Handlers
{
	public class OwnApplicationsRequirementHandler : AuthorizationHandler<OwnApplicationsRequirement, Application>
	{
		private readonly ICurrentUserService _currentUserService;

		public OwnApplicationsRequirementHandler(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnApplicationsRequirement requirement, Application resource)
		{
			// Admin bypass
			if (context.User.IsInRole("Admin"))
			{
				context.Succeed(requirement);
				return Task.CompletedTask;
			}
			// Ownership checks
			if (resource == null ||
				resource.UserId != _currentUserService.GetCurrentUserId())
			{
				context.Fail();
				return Task.CompletedTask;
			}

			context.Succeed(requirement);
			return Task.CompletedTask;
		}
	}
}
