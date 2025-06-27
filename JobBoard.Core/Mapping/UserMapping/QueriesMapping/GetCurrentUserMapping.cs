using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.UserMapping
{
	public partial class UserProfile
	{
		public void GetCurrentUserMapping()
		{
			CreateMap<User, GetCurrentUserQueryResponse>()
				.ForMember(dst => dst.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
				.ForMember(dst => dst.CountryName, opt => opt.MapFrom(src => src.Country!.CountryName.ToString()));

		}
	}
}
