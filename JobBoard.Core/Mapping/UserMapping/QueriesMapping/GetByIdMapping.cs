using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.UserMapping
{
	public partial class UserProfile
	{
		public void AddGetByIdMapping()
		{
			CreateMap<User, GetUserByIdQueryResponse>()
				.ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dst => dst.Gendor, opt => opt.MapFrom(src => src.Gender.ToString()))
				.ForMember(dst => dst.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToShortDateString() : null))
				.ForMember(dst => dst.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.CountryName : ""));
		}
	}
}
