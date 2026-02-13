using JobBoard.Core.Authrization.Requirements;
using JobBoard.Data.Entities;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Authrization.Handlers
{
	public class CompanyOwnerHandler : AuthorizationHandler<CompanyCreatorRequirement, Company>
	{
		private readonly ICurrentUserService _currentUserService;

		public CompanyOwnerHandler(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CompanyCreatorRequirement requirement, Company resource)
		{
			var userId = _currentUserService.GetCurrentUserId();

			if (resource.CreatedByUserId.Equals(userId))
				context.Succeed(requirement);

			return Task.CompletedTask;


		}
	}
}
