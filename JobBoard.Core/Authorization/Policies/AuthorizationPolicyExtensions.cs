using JobBoard.Core.Authrization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Authorization.Policies
{
	public static class AuthorizationPolicyExtensions
	{
		/// <summary>
		/// Registers all application-specific authorization policies
		/// </summary>
		/// <param name="options"></param>
		public static void AddApplicationPolicies(this AuthorizationOptions options)
		{
			// Users can access only their own applications
			options.AddPolicy(AuthorizationPolicies.CanAccessOwnApplications, policy =>
				policy.Requirements.Add(new OwnApplicationsRequirement()));

			// Users can access companies they created
			options.AddPolicy(AuthorizationPolicies.IsCompanyCreator, policy =>
				policy.Requirements.Add(new CompanyCreatorRequirement()));

			// Users can access files they own
			options.AddPolicy(AuthorizationPolicies.IsFileOwner, policy =>
				policy.Requirements.Add(new FileOwnerRequirement()));

			// Users can access jobs they created
			options.AddPolicy(AuthorizationPolicies.IsJobCreator, policy =>
				policy.Requirements.Add(new JobOwnerRequirement()));

			// Users can access their own bookmarks
			options.AddPolicy(AuthorizationPolicies.CanAccessOwnBookmarks, policy =>
				policy.Requirements.Add(new OwnBookmarkRequirement()));
		}
	}
}
