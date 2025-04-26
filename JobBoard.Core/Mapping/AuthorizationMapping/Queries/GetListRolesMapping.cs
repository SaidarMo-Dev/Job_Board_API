using JobBoard.Core.Feutures.Authorization.Queries.Responses;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.AuthorizationMapping
{
	public partial class AuthorizationProfile
	{
		public void GetListRolesMapping()
		{
			CreateMap<Role, GetListRolesQueryRsponse>();
		}
	}
}
