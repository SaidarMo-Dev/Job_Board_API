using System.Reflection;
using FluentValidation;
using JobBoard.Core.Authorization.Policies;
using JobBoard.Core.Authrization.Handlers;
using JobBoard.Core.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
namespace JobBoard.Core
{

	public static class ModeCoreDependencies
	{
		public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
		{
			services.AddMediatR(
						cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			services.AddAutoMapper(Assembly.GetExecutingAssembly());


			// configuration of fluent validating 
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			// 
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


			//services.AddAuthorization(option =>
			//{
			//	option.AddPolicy("Edit", policy =>
			//	{
			//		policy.RequireClaim("Edit", ["EditJob", "EditUser", "EditCompany"]);

			//	});

			//	option.AddPolicy("Get", policy =>
			//	{
			//		policy.RequireClaim("Get", "GetJob");
			//	});
			//});


			// resource based authorization

			services.AddScoped<IAuthorizationHandler, SameUserRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, JobOwnerRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, CompanyCreatorRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, OwnBookmarkRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, OwnApplicationsRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, FileOwnerRequirementHandler>();

			// Registrate policies

			services.AddAuthorization(options =>
			{
				options.AddApplicationPolicies();
			});

			return services;
		}
	}
}