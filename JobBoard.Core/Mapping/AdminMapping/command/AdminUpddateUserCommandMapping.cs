using JobBoard.Core.Feutures.Admin.Command.Models;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.AdminMapping
{
	public partial class AdminProfile
	{
		public void AdminUpddateUserCommandMapping()
		{
			CreateMap<AdminUpdateUserCommand, User>();
		}
	}
}
