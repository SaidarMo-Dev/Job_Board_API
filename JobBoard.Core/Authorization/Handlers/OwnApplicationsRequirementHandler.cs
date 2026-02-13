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
			var userId = _currentUserService.GetCurrentUserId();

			if (resource.UserId.Equals(userId))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
