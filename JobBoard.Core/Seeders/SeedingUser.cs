using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Core.Seeders
{
	public static class UserSeeder
	{
		public static async Task SeedAsync(UserManager<User> userManager)
		{
			var userCount = await userManager.Users.CountAsync();
			if (userCount == 0)
			{
				var user = new User()
				{
					FirstName = "Mohammed",
					LastName = "Saidar",
					Gendor = GendorEnum.Male,
					DateOfBirth = DateTime.UtcNow.AddYears(-24),
					Address = "Morocco",
					ImagePath = "",
					IsDeleted = false,
					UserName = "Admin",
					Email = "AdminProject@gmail.com",
					PhoneNumber = "+323 445554433",
					CountryId = 119,
					EmailConfirmed = true,
					PhoneNumberConfirmed = true

				};

				await userManager.CreateAsync(user, "Admin@1234");
				await userManager.AddToRoleAsync(user, "Admin");

			}
		}
	}
}
