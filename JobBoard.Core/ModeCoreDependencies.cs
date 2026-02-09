using System.Reflection;
using FluentValidation;
using JobBoard.Core.Behaviors;
using JobBoard.Core.Security.Handlers;
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


			services.AddAuthorization(option =>
			{
				option.AddPolicy("Edit", policy =>
				{
					policy.RequireClaim("Edit", ["EditJob", "EditUser", "EditCompany"]);

				});

				option.AddPolicy("Get", policy =>
				{
					policy.RequireClaim("Get", "GetJob");
				});
			});


			// resource based authorization

			services.AddScoped<IAuthorizationHandler, SameUserHandler>();
			services.AddScoped<IAuthorizationHandler, JobCreatorHandler>();
			services.AddScoped<IAuthorizationHandler, CompanyOwnerHandler>();
			services.AddScoped<IAuthorizationHandler, UserBookmarkHandler>();
			services.AddScoped<IAuthorizationHandler, UserApplicationsHandler>();
			services.AddScoped<IAuthorizationHandler, FileOwnershipHandler>();

			return services;
		}
	}
}