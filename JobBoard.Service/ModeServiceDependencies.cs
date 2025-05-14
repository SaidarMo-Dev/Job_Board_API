using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Implementations;
using JobBoard.Service.Authentication.Interfaces;
using JobBoard.Service.Authorization;
using JobBoard.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace JobBoard.Service
{
	public static class ModeServiceDependencies
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{
			services.AddScoped<ICountryService, CountryService>();
			services.AddScoped<ICompanyService, CompanyService>();
			services.AddScoped<ISkillService, SkillService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IJobService, JobService>();
			services.AddScoped<IJobSkillService, JobSkillService>();
			services.AddScoped<IJobCategoryService, JobCategoryService>();
			services.AddScoped<IBookmarkService, BookmarkService>();
			services.AddScoped<IApplicationService, ApplicationService>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<IAuthorizationService, AuthorizationService>();
			services.AddScoped<IEmailService, EmailService>();

			return services;
		}
	}
}
