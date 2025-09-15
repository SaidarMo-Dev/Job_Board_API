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
				options.SignIn.RequireConfirmedEmail = true;

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
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = jwtSettings.ValidateIssuer,
					ValidIssuer = jwtSettings.Issuer,
					ValidateAudience = jwtSettings.ValidateAudience,
					ValidAudience = jwtSettings.Audience,
					ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigninKey,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
					ValidateLifetime = jwtSettings.ValidateLifeTime
				};

				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						// Read from cookie
						if (context.Request.Cookies.ContainsKey("accessToken"))
						{
							context.Token = context.Request.Cookies["accessToken"];
						}
						return Task.CompletedTask;
					}
				};
			});

			// Configuration of Swager Gen

			services.AddSwaggerGen(x =>
			{
				x.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Job Board APIs",
					Version = "v1",
					Description = "The **Job Board API** provides a comprehensive set of endpoints for managing job postings, applications and more...",
					Contact = new OpenApiContact
					{
						Name = "Mohammed Saidar",
						Email = "saidarmohammedeco@gmail.com"
					}
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
