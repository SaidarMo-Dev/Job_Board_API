using AutoMapper;

namespace JobBoard.Core.Mapping.AdminMapping
{
	public partial class AdminProfile : Profile
	{
		public AdminProfile()
		{
			GetUsersQueryMapping();
			AdminAddUserCommandMapping();
			AdminUpddateUserCommandMapping();
		}
	}
}
