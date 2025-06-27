using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.UserMapping
{
	public partial class UserProfile
	{
		public void UpdateUserCommandMapping()
		{
			CreateMap<UpdateUserCommand, User>()
				.ForMember(dst => dst.Gender, opt => opt.MapFrom(src => getGender(src.Gender.ToLower())));


		}

		public int getGender(string gender)
		{
			if (gender == "male") return 0;
			else if (gender == "female") return 1;
			else return 2;
		}
	}
}
