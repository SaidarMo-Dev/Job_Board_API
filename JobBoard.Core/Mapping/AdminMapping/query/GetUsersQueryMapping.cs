using JobBoard.Core.Feutures.Admin.Query.Responses;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.AdminMapping
{
	public partial class AdminProfile
	{
		public void GetUsersQueryMapping()
		{
			CreateMap<User, GetUsersQueryResponse>().ForMember(dst => dst.country, opt => opt.MapFrom(src => src.Country != null ? src.Country.CountryName : null));
		}
	}
}
