using JobBoard.Core.Authrization.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Authrization.Handlers
{
	public class OwnBookmarkRequirementHandler : AuthorizationHandler<OwnBookmarkRequirement, Bookmark>
	{

		private readonly ICurrentUserService _currentUserService;

		public OwnBookmarkRequirementHandler(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnBookmarkRequirement requirement, Bookmark resource)
		{
			// Admin bypass
			if (context.User.IsInRole("Admin"))
			{
				context.Succeed(requirement);
				return Task.CompletedTask;
			}

			// Validate resource
			if (resource == null)
			{
				context.Fail();
				return Task.CompletedTask;
			}

			// Get current user id
			var currentUserId = _currentUserService.GetCurrentUserId();

			// Ownership check
			if (resource.UserId == currentUserId)
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}

			return Task.CompletedTask;
		}
	}
}
