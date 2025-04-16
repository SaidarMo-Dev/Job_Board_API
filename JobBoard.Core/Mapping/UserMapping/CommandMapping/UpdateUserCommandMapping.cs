using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.UserMapping
{
	public partial class UserProfile
	{
		public void UpdateUserCommandMapping()
		{
			CreateMap<UpdateUserCommand, User>();

		}
	}
}
