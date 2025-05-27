using JobBoard.Core.Security.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Security.Handlers
{
	public class JobCreatorHandler : AuthorizationHandler<JobCreatorRequirement, JobListing>
	{
		private readonly ICurrentUserService _currentUserService;

		public JobCreatorHandler(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JobCreatorRequirement requirement, JobListing resource)
		{
			var userId = _currentUserService.GetCurrentUserId();

			if (resource.CreatedByUserId == userId)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
