namespace JobBoard.Data.Metadata
{
	public static class Router
	{
		private const string Api = "/Api";
		private const string Version = "/V1";
		private const string Rule = Api + Version;
		private const string Single = "/{Id}";

		public static class CompanyRoute
		{
			private const string Prefex = Rule + "/Company";

			public const string GetAll = Prefex + "/GetAll";
			public const string Paginate = Prefex + "/Paginate";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddNewCompany";
			public const string Update = Prefex + "/UpdateCompany";
			public const string DeleteById = Prefex + Single;
			public const string Jobs = Prefex + Single + "/Jobs";

		}
		public static class SkillRoute
		{
			private const string Prefex = Rule + "/Skill";

			public const string GetAll = Prefex + "/GetAll";
			public const string Paginate = Prefex + "/Paginate";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddSkill";
			public const string Update = Prefex + "/UpdateSkill";
			public const string DeleteById = Prefex + Single;

		}

		public static class CategoryRoute
		{
			private const string Prefex = Rule + "/Categories";

			public const string GetAll = Prefex + "/GetAll";
			public const string Paginate = Prefex + "/Paginate";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddCategory";
			public const string Update = Prefex + "/UpdateCategory";
			public const string DeleteById = Prefex + Single;

		}


		public static class ApplicationUserRoute
		{
			private const string Prefex = Rule + "/Users";


			public const string GetAll = Prefex + "/GetAll";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddUser";
			public const string Update = Prefex + "/UpdateUser";
			public const string DeleteById = Prefex + Single;
			public const string Paginate = Prefex + "/Paginate";
			public const string ChangePassword = Prefex + "/ChangePassword";
			public const string Bookmarks = Prefex + "/Bookmarks/{Id}";
			public const string Applications = Prefex + "/Applications";


		}
		public static class JobRoute
		{
			private const string Prefex = Rule + "/Jobs";

			public const string GetAll = Prefex + "/GetAll";
			public const string GetByID = Prefex + Single;
			public const string Create = Prefex + "/AddJob";
			public const string Update = Prefex + "/UpdateJob";
			public const string DeleteById = Prefex + Single;
			public const string Paginate = Prefex + "/Paginate";
			public const string Skills = Prefex + "/Skills";
			public const string Categories = Prefex + "/Categories";

			public const string Applications = Prefex + Single + "/Applications";

		}


		public static class BookMarkRoute
		{
			private const string Prefex = Rule + "/Bookmarks";

			public const string GetAll = Prefex + "/GetAll";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddBookmark";
			public const string DeleteById = Prefex + Single;
			public const string Paginate = Prefex + "/Paginate";
		}

		public static class ApplicationRoute
		{
			private const string Prefex = Rule + "/Applications";

			public const string GetAll = Prefex + "/GetAll";
			public const string GetByID = Prefex + "/{Id}";
			public const string Apply = Prefex + "/AddApplication";
			public const string Update = Prefex + "/UpdateApplication";
			public const string DeleteById = Prefex + Single;
			public const string Paginate = Prefex + "/Paginate";

			public const string SetAccepted = Prefex + "/SetAccepted";
			public const string SetRemoved = Prefex + "/SetRemoved";
		}


		public static class AuthenticationRoute
		{
			private const string Prefex = Rule + "/Authentication";
			public const string ConfirmEmail = Prefex + "/ConfirmEmail";
			public const string SignIn = Prefex + "/SignIn";
			public const string RefreshToken = Prefex + "/RefreshToken";
			public const string SendResetPassword = Prefex + "/SendResetPassword";
			public const string ConfirmResetPassword = Prefex + "/ConfirmResetPassword";
			public const string ResetPassword = Prefex + "/ResetPassword";
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

			public const string SendEmail = Prefex + "/SendEmail";
		}
	}
}
