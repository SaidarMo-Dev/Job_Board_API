using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace JobBoard.Infrastructure
{
	public static class AddRegistrations
	{
		public static IServiceCollection AddRegistration(this IServiceCollection services)
		{

			services.AddIdentity<User, IdentityRole<int>>(options =>
			{
				options.SignIn.RequireConfirmedEmail = true;

				// password configuration 
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 8;

				options.User.RequireUniqueEmail = true;

			}).AddEntityFrameworkStores<appDbContext>().AddDefaultTokenProviders();
			return services;
		}
	}
}
