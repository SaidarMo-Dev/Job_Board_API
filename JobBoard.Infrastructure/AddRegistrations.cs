using System.Text;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Infrastructure.context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace JobBoard.Infrastructure
{
	public static class AddRegistrations
	{
		public static IServiceCollection AddRegistration(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddIdentity<User, Role>(options =>
			{
				options.SignIn.RequireConfirmedEmail = false;

				// password configuration 
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 8;

				options.User.RequireUniqueEmail = true;

			}).AddEntityFrameworkStores<appDbContext>().AddDefaultTokenProviders();

			// add jwt settings
			var jwtSettings = new JwtSettings();
			configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);

			services.AddSingleton(jwtSettings);

			// add email settings
			var emailSettings = new EmailSettings();
			configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);

			services.AddSingleton(emailSettings);


			// configuration Authentication
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(X =>
			{
				X.RequireHttpsMetadata = false;
				X.SaveToken = true;
				X.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = jwtSettings.ValidateIssuer,
					ValidIssuer = jwtSettings.Issuer,
					ValidateAudience = jwtSettings.ValidateAudience,
					ValidAudience = jwtSettings.Audience,
					ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigninKey,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
					ValidateLifetime = jwtSettings.ValidateLifeTime
				};

			});

			//configuration of Swager Gen

			services.AddSwaggerGen(x =>
			{
				x.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "API",
					Version = "v1"

				});

				x.EnableAnnotations();

				x.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme {Exemple : 'Bearer12345abcdef'}",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = JwtBearerDefaults.AuthenticationScheme
				});

				x.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id= JwtBearerDefaults.AuthenticationScheme
							}
						},
						Array.Empty<string>()
					}
				});
			});
			return services;
		}
	}
}
