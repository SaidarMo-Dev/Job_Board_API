namespace JobBoard.Data.Responses
{
	public class ManageUserRolesDto
	{
		public int UserId { get; set; }
		public List<RoleResponse> Roles { get; set; }

	}

	public class RoleResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool HasRodle { get; set; }
	}

}
