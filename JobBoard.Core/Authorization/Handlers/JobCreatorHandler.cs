using JobBoard.Core.Authrization.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Data.Helpers;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Authrization.Handlers
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

			var userId = context.User.FindFirst(c => c.Type == nameof(JwtClaimModel.UserId))?.Value;


			if (resource.CreatedByUserId == int.Parse(userId ?? "-1"))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
