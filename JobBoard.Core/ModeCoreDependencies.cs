using System.Reflection;
using FluentValidation;
using JobBoard.Core.Behaviors;
using MediatR;
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


			return services;
		}
	}
}