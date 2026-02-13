using JobBoard.Core.Authrization.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Authrization.Handlers
{
	public class UserBookmarkHandler : AuthorizationHandler<OwnBookmarkRequirement, Bookmark>
	{

		private readonly ICurrentUserService _currentUserService;

		public UserBookmarkHandler(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnBookmarkRequirement requirement, Bookmark resource)
		{
			var userId = _currentUserService.GetCurrentUserId();

			if (resource.UserId.Equals(userId))
				context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}
}
