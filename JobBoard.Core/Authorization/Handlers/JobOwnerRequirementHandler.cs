using JobBoard.Core.Authrization.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Data.Helpers;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Authrization.Handlers
{
	public class JobOwnerRequirementHandler : AuthorizationHandler<JobOwnerRequirement, JobListing>
	{

		public JobOwnerRequirementHandler(ICurrentUserService currentUserService)
		{

		}
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JobOwnerRequirement requirement, JobListing resource)
		{

			var userId = context.User.FindFirst(c => c.Type == JwtClaimTypes.UserId)?.Value;


			if (resource.CreatedByUserId == int.Parse(userId ?? "-1"))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
