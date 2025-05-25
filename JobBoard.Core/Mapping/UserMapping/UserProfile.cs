using AutoMapper;

namespace JobBoard.Core.Mapping.UserMapping
{
	public partial class UserProfile : Profile
	{
		public UserProfile()
		{
			AddCreateUserCommandMapping();
			AddGetByIdMapping();
			GetPaginatedListUsersQuery();
			UpdateUserCommandMapping();

		}
	}
}
