using JobBoard.Service.Abstractions;
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


			return services;
		}
	}
}
