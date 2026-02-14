using JobBoard.Core.Authrization.Requirements;
using JobBoard.Core.Authrization.Resources;
using JobBoard.Data.Helpers;
using JobBoard.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Authrization.Handlers
{
	public class FileOwnerRequirementHandler : AuthorizationHandler<FileOwnerRequirement, FileUploadResource>
	{
		private readonly ICompanyService _companyService;

		public FileOwnerRequirementHandler(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, FileOwnerRequirement requirement, FileUploadResource resource)
		{
			// Authorizes access to a resource based on ownership and type.
			// Users can access their own resources, any authenticated user can access applications/jobs,
			// and only company creators can access company resources.

			// Admin bypass
			if (context.User.IsInRole("Admin"))
			{
				context.Succeed(requirement);
				return;
			}

			// Validate user id claim
			var userIdClaim = context.User.FindFirst(c => c.Type == JwtClaimTypes.UserId)?.Value;

			if (!int.TryParse(userIdClaim, out var currentUserId))
			{
				context.Fail();
				return;
			}

			// Validate resource
			if (resource == null)
			{
				context.Fail();
				return;
			}

			// Authorization logic
			switch (resource.OwnerType)
			{
				case Data.enums.FileOwnerType.Users:
					if (resource.OwnerId == currentUserId)
						context.Succeed(requirement);
					else
						context.Fail();
					return;

				case Data.enums.FileOwnerType.Applications:
				case Data.enums.FileOwnerType.Jobs:
					context.Succeed(requirement);
					return;

				case Data.enums.FileOwnerType.Companies:
					var isCompanyOwner =
						await _companyService.IsCreatedByUserAsync(resource.OwnerId, currentUserId);

					if (isCompanyOwner)
						context.Succeed(requirement);
					else
						context.Fail();
					return;

				default:
					context.Fail();
					return;
			}


		}
	}
}
