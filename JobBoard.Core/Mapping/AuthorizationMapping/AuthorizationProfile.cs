using AutoMapper;

namespace JobBoard.Core.Mapping.AuthorizationMapping
{
	public partial class AuthorizationProfile : Profile
	{
		public AuthorizationProfile()
		{
			GetListRolesMapping();
			GetSingleRoleQueryMapping();
		}
	}
}
