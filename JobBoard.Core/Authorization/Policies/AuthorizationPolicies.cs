namespace JobBoard.Core.Authorization.Policies
{
	public static class AuthorizationPolicies
	{
		/// <summary>
		/// Allows users to access only their own applications
		/// </summary>
		public const string SameUser = "SameUserPolicy";

		/// <summary>
		/// Allows users to access only their own applications
		/// </summary>
		public const string CanAccessOwnApplications = "CanAccessOwnApplicationsPolicy";

		/// <summary>
		/// Allows users to access companies they created
		/// </summary>
		public const string IsCompanyCreator = "IsCompanyCreatorPolicy";

		/// <summary>
		/// Allows users to access files they own
		/// </summary>
		public const string IsFileOwner = "IsFileOwnerPolicy";

		/// <summary>
		/// Allows users to access jobs they created
		/// </summary>
		public const string IsJobCreator = "IsJobCreatorPolicy";

		/// <summary>
		/// Allows users to access their own bookmarks
		/// </summary>
		public const string CanAccessOwnBookmarks = "CanAccessOwnBookmarksPolicy";
	}
}
