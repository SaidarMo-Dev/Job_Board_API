using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobBoard.Infrastructure
{
	public static class ModelInfrastructureDependencies
	{
		public static IServiceCollection AddInfrastuctureDependencies(this IServiceCollection services)
		{
			services.AddScoped<ICountryRepository, CountryRepository>();
			services.AddScoped<ICompanyRepository, CompanyRepository>();
			services.AddScoped<ISkillRepository, SkillRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IJobRepository, JobRepository>();
			services.AddScoped<IJobSkillRepository, JobSkillRepository>();
			services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
			services.AddScoped<IBookMarkRepository, BookMarkRepository>();
			services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
			services.AddScoped<IApplicationRepository, ApplicationRepository>();

			return services;
		}
	}
}
