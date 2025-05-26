using JobBoard.Core.Security.Requirements;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Security.Handlers
{
	public class SameUserHandler : AuthorizationHandler<SameUserRequirement, User>
	{
		private readonly ICurrentUserService _currentUserService;

		public SameUserHandler(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
														SameUserRequirement requirement,
														User resource)
		{
			var UserId = _currentUserService.GetCurrentUserId();

			if (resource.Id == UserId)
			{
				context.Succeed(requirement);
			}
			return Task.CompletedTask;
		}
	}
}
