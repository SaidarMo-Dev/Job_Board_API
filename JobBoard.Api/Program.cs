using System.Globalization;
using JobBoard.Core;
using JobBoard.Core.Middleware;
using JobBoard.Core.Security.Handlers;
using JobBoard.Core.Seeders;
using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure;
using JobBoard.Infrastructure.context;
using JobBoard.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure appDbContext 
builder.Services.AddDbContext<appDbContext>(
	options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

#region Serilog Connfiguration

Log.Logger = new LoggerConfiguration().ReadFrom
		.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddSerilog();

#endregion

#region Cors configurations

builder.Services.AddCors(options =>
{
	options.AddPolicy("MyPolicy", builder =>
	{

		builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials();

	});
});

#endregion

#region  Dependencies 

builder.Services.AddInfrastuctureDependencies()
	.AddServiceDependencies()
	.AddCoreDependencies()
	.AddRegistration(builder.Configuration);

#endregion


#region Depen For IUrlHelper
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IUrlHelper>(x =>
{
	var actionContext = x.GetService<IActionContextAccessor>().ActionContext;
	var factory = x.GetRequiredService<IUrlHelperFactory>();
	return factory.GetUrlHelper(actionContext);

});

#endregion

#region Localization Configurations

builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
	opt.ResourcesPath = "";

});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	List<CultureInfo> supportedCultures = new()
	{
		new CultureInfo("en-US"),
		new CultureInfo("ar-MA"),
		new CultureInfo("fr-FR"),
		new CultureInfo("de-DE")
	};

	options.DefaultRequestCulture = new RequestCulture("ar-MO");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;

});
#endregion


builder.Services.AddScoped<IAuthorizationHandler, SameUserHandler>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<appDbContext>();
	context.Database.Migrate();
}


#region Seeders
using (var service = app.Services.CreateScope())
{
	var userManager = service.ServiceProvider.GetRequiredService<UserManager<User>>();
	var roleManager = service.ServiceProvider.GetRequiredService<RoleManager<Role>>();

	await RoleSeeder.SeedAsync(roleManager);
	await UserSeeder.SeedAsync(userManager);


}

#endregion


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//app.UseSwagger(options =>
//{
//	options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
//});
//app.UseSwaggerUI();

//}

app.UseSwagger(options =>
{
	options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
});
app.UseSwaggerUI();


#region Localization Midllware

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(options.Value);

#endregion
app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
