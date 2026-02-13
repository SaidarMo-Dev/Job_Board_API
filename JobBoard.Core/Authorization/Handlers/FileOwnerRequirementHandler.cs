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


			var userIdClaim = context.User.FindFirst(c => c.Type == nameof(JwtClaimModel.UserId))?.Value;

			if (!int.TryParse(userIdClaim, out var currentUserId))
			{
				context.Fail();
				return;
			}

			bool isAuthorized;

			switch (resource.OwnerType)
			{
				case Data.enums.FileOwnerType.Users:
					isAuthorized = resource.OwnerId == currentUserId;
					break;

				case Data.enums.FileOwnerType.Applications:
				case Data.enums.FileOwnerType.Jobs:
					isAuthorized = true;
					break;

				case Data.enums.FileOwnerType.Companies:
					isAuthorized = await _companyService.IsCreatedByUserAsync(resource.OwnerId, currentUserId);
					break;

				default:
					isAuthorized = false;
					break;
			}

			if (isAuthorized)
				context.Succeed(requirement);
			else
				context.Fail();

		}
	}
}
