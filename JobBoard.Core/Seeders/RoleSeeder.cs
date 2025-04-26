using JobBoard.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Core.Seeders
{
	public static class RoleSeeder
	{
		public static async Task SeedAsync(RoleManager<Role> roleManager)
		{
			var roleCount = await roleManager.Roles.CountAsync();

			if (roleCount == 0)
			{
				await roleManager.CreateAsync(new Role { Name = "Admin" });
				await roleManager.CreateAsync(new Role { Name = "User" });
			}
		}
	}
}
