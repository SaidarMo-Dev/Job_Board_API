using JobBoard.Data.Helpers;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Implementations;
using JobBoard.Service.Authentication.Interfaces;
using JobBoard.Service.Authorization;
using JobBoard.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Supabase;

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
			services.AddScoped<ICurrentUserService, CurrentUserService>();
			services.AddScoped<IEmailJobService, EmailJobService>();
			services.AddScoped<ITokenHelper, TokenHelper>();
			services.AddScoped<IFileStorageService, SupabaseFileStorageService>();
			services.AddScoped<IFileResourceService, FileResourceService>();
			services.AddScoped<IFileUrlResolver, FileUrlResolver>();
			services.AddScoped<IIndustryService, IndustryService>();


			services.AddSingleton<Client>(sp =>
			{
				var settings = sp.GetRequiredService<IOptions<SupabaseSettings>>().Value;

				var client = new Client(
					settings.Url,
					settings.ServiceKey,
					new SupabaseOptions
					{
						AutoConnectRealtime = false
					});

				return client;
			});

			services.AddMemoryCache();

			services.AddScoped<ISignedUrlCache, MemorySignedUrlCache>();
			services.AddScoped<ICompanyFileStitcher, CompanyFileStitcher>();

			return services;
		}
	}
}
