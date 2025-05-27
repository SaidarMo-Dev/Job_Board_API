using JobBoard.Core.Security.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Security.Handlers
{
	public class UserBookmarkHandler : AuthorizationHandler<UserBookmarkRequirement, Bookmark>
	{

		private readonly ICurrentUserService _currentUserService;

		public UserBookmarkHandler(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserBookmarkRequirement requirement, Bookmark resource)
		{
			var userId = _currentUserService.GetCurrentUserId();

			if (resource.UserId.Equals(userId))
				context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}
}
