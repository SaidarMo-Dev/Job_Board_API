using JobBoard.Core.Feutures.Admin.Query.Responses;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.AdminMapping
{
	public partial class AdminProfile
	{
		public void GetAdminProfileQueryMapping()
		{
			CreateMap<User, GetAdminProfileQueryResponse>()

				.ForMember(x => x.Country, opt => opt.MapFrom(src => src.Country != null ? src.Country.CountryName : null));


		}
	}
}
