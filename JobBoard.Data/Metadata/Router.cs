namespace JobBoard.Data.Metadata
{
	public static class Router
	{
		private const string Api = "Api";
		private const string Version = "/V1";
		private const string Rule = Api + Version;

		public static class CompanyRoute
		{
			private const string Prefex = Rule + "/Company";

			public const string GetAll = Prefex + "/GetAll";
			public const string Paginate = Prefex + "/Paginate";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddNewCompany";
			public const string Update = Prefex + "/UpdateCompany";
			public const string DeleteById = Prefex + "/Delete/{Id}";

		}
		public static class SkillRoute
		{
			private const string Prefex = Rule + "/Skill";

			public const string GetAll = Prefex + "/GetAll";
			public const string Paginate = Prefex + "/Paginate";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddSkill";
			public const string Update = Prefex + "/UpdateSkill";
			public const string DeleteById = Prefex + "/Delete/{Id}";

		}

		public static class CategoryRoute
		{
			private const string Prefex = Rule + "/Categories";

			public const string GetAll = Prefex + "/GetAll";
			public const string Paginate = Prefex + "/Paginate";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddCategory";
			public const string Update = Prefex + "/UpdateCategory";
			public const string DeleteById = Prefex + "/Delete/{Id}";

		}


		public static class ApplicationUserRoute
		{
			private const string Prefex = Rule + "/Users";

			public const string GetAll = Prefex + "/GetAll";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddUser";
			public const string Update = Prefex + "/UpdateUser";
			public const string DeleteById = Prefex + "/Delete/{Id}";
			public const string Paginate = Prefex + "/Paginate";

		}
		public static class JobRoute
		{
			private const string Prefex = Rule + "/Jobs";

			public const string GetAll = Prefex + "/GetAll";
			public const string GetByID = Prefex + "/{Id}";
			public const string Create = Prefex + "/AddJob";
			public const string Update = Prefex + "/UpdateJob";
			public const string DeleteById = Prefex + "/Delete/{Id}";
			public const string Paginate = Prefex + "/Paginate";
			public const string Skills = Prefex + "/Skills";
			public const string Categories = Prefex + "/Categories";

		}

	}
}
