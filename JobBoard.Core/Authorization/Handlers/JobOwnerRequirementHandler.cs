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

			// Extract user id safely
			var userIdClaim = context.User
				.FindFirst(c => c.Type == JwtClaimTypes.UserId)?.Value;

			if (!int.TryParse(userIdClaim, out var currentUserId))
			{
				context.Fail();
				return Task.CompletedTask;
			}

			// Ownership check
			if (resource.CreatedByUserId == currentUserId)
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
