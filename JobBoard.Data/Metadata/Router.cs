namespace JobBoard.Data.Metadata
{
	public static class Router
	{
		private const string Api = "/Api";
		private const string Version = "/V1";
		private const string Rule = Api + Version;
		private const string Single = "/{Id}";


		public static class AdminRoute
		{
			private const string Prefex = Rule + "/admin";

			public const string GetUsers = Prefex + "/users";
			public const string AddUser = Prefex + "/users";
			public const string UpdateUser = Prefex + "/users";
			public const string Profile = Prefex + "/profile";
			public const string GetJobs = Prefex + "/jobs";


		}

		public static class CountryRoute
		{
			private const string Prefex = Rule + "/Country";

			public const string GetAll = Prefex + "/GetAll";
			public const string GetByID = Prefex + "/{Id}";

		}

		public static class CompanyRoute
		{
			private const string Prefex = Rule + "/companies";

			public const string GetAll = Prefex;
			public const string Paginate = Prefex + "/paginate";
			public const string GetByID = Prefex + Single;
			public const string Create = Prefex;
			public const string Update = Prefex;
			public const string DeleteById = Prefex + Single;
			public const string Jobs = Prefex + Single + "/Jobs";

			public const string PopularCompanies = Prefex + "/popular";
			public const string CompaniesSummary = Prefex + "/summary";

		}
		public static class SkillRoute
		{
			private const string Prefex = Rule + "/skills";

			public const string GetAll = Prefex;
			public const string Paginate = Prefex + "/paginate";
			public const string Summary = Prefex + "/summary";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex;
			public const string Update = Prefex;
			public const string DeleteById = Prefex + Single;

		}

		public static class CategoryRoute
		{
			private const string Prefex = Rule + "/categories";

			public const string GetAll = Prefex;
			public const string Popular = Prefex + "/popular";
			public const string summary = Prefex + "/summary";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex;
			public const string Update = Prefex;
			public const string DeleteById = Prefex + Single;

		}


		public static class ApplicationUserRoute
		{
			private const string Prefex = Rule + "/users";


			public const string GetAll = Prefex + "/get-all";
			public const string GetByID = Prefex + "/{Id}";
			public const string me = Prefex + "/me";
			public const string Register = Prefex + "/register";
			public const string Update = Prefex + "/update";
			public const string DeleteById = Prefex + Single;
			public const string Paginate = Prefex + "/paginate";
			public const string Bookmarks = Prefex + "/bookmarks";
			public const string TotaleBookmarks = Prefex + "/bookmarks/Count";
			public const string Applications = Prefex + "/applications";
			public const string DashboardStats = Prefex + "/{Id}/dashboard-stats";


		}
		public static class JobRoute
		{
			private const string Prefex = Rule + "/Jobs";

			public const string GetAll = Prefex;
			public const string GetByID = Prefex + Single;
			public const string Create = Prefex + "/AddJob";
			public const string Update = Prefex + "/UpdateJob";
			public const string DeleteById = Prefex + Single;
			public const string Paginate = Prefex + "/Paginate";
			public const string Skills = Prefex + "/Skills";
			public const string Categories = Prefex + "/Categories";

			public const string Applications = Prefex + Single + "/Applications";
			public const string Locations = Prefex + "/locations";
			public const string Recommendations = Prefex + "/recommendations";

		}


		public static class BookMarkRoute
		{
			private const string Prefex = Rule + "/bookmarks";

			public const string GetAll = Prefex;
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/save-job";
			public const string DeleteById = Prefex + Single;
			public const string DeleteByJobId = Prefex + "/by-jobId" + Single;
			public const string Paginate = Prefex + "/paginate";
			public const string UserSavedJobIds = Prefex + "/{Id}/saved-job-ids";

			public const string RecentSavedJobs = Prefex + "/recent-saved-jobs";

		}

		public static class ApplicationRoute
		{
			private const string Prefex = Rule + "/applications";

			public const string GetAll = Prefex;
			public const string GetByID = Prefex + "/{Id}";
			public const string Apply = Prefex + "/apply";
			public const string Update = Prefex + "/update-application";
			public const string DeleteById = Prefex + Single;
			public const string Paginate = Prefex + "/paginate";

			public const string SetAccepted = Prefex + "/set-accepted";
			public const string SetRemoved = Prefex + "/set-removed";

			public const string RecentApplications = Prefex + "/recent-applications";

			public const string AppliedJobIds = Prefex + "/applied-job-ids";

		}


		public static class AuthenticationRoute
		{
			private const string Prefex = Rule + "/auth";
			public const string ConfirmEmailByUrl = Prefex + "/confirm-email-url";
			public const string ConfirmEmailByCode = Prefex + "/confirm-email-code";
			public const string SignIn = Prefex + "/signin";
			public const string RefreshToken = Prefex + "/refresh-token";
			public const string SendResetPassword = Prefex + "/send-reset-password";
			public const string ConfirmResetPassword = Prefex + "/confirm-reset-password";
			public const string ResetPassword = Prefex + "/reset-password";
			public const string SendConfirmeEmail = Prefex + "/send-confirm-email";

			public const string SendConfirmeEmailCode = Prefex + "/send-confirm-email-code";
			public const string SendEmailChange = Prefex + "/send-email-change";
			public const string VerifyEmailChange = Prefex + "/verify-email-change";

			public const string ChangePassword = Prefex + "/change-password";

			public const string AddRecoveryContact = Prefex + "/add-recovery-contact";

			public const string VerfiyPassword = Prefex + "/verify-password";
			public const string ResendVerificationCode = Prefex + "/resend-verification-code";

			public const string Logout = Prefex + "/logout";

		}


		public static class AuthorizationRoute
		{
			private const string Prefex = Rule + "/Authorization";
			private const string Roles = Prefex + "/Roles";
			private const string Claims = Prefex + "/Claims";

			public const string Create = Roles + "/Add-Role";
			public const string Update = Roles + "/Update-Role";
			public const string DeleteById = Roles + Single;
			public const string GetAllRoles = Roles + "/GetAll";
			public const string GetRoleById = Roles + Single;
			public const string GetManageUserRoles = Roles + "/ManageUserRoles/{Id}";
			public const string UpdateUserRoles = Roles + "/UpdateUserRoles";
			public const string ManageUserClaims = Claims + "/Manage-user-claims/{Id}";
			public const string UpdateUserClaims = Claims + "/Update-user-claims";

		}

		public static class EmailRoute
		{
			private const string Prefex = Rule + "/Email";

			public const string SendEmail = Prefex + "/send-email";
		}
	}
}
