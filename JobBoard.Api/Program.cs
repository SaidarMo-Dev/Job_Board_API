using System.Globalization;
using System.Text.Json.Serialization;
using Hangfire;
using JobBoard.Core;
using JobBoard.Core.Middleware;
using JobBoard.Core.Seeders;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Infrastructure;
using JobBoard.Infrastructure.context;
using JobBoard.Infrastructure.ModelBinders;
using JobBoard.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers((options) =>
{
	options.ModelBinderProviders.Insert(0, new CaseInsensitiveFormModelBinderProvider());

})
.AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SupabaseSettings
builder.Services.Configure<SupabaseSettings>(
				builder.Configuration.GetSection("Supabase"));

#region Configure appDbContext 
builder.Services.AddDbContext<appDbContext>(
	options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
#endregion

#region Hangfire configuration

builder.Services.AddHangfire(config =>
	config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();


#endregion

#region Cors configurations

builder.Services.AddCors(options =>
{
	options.AddPolicy("iLinkApiCors", builder =>
	{

		builder.WithOrigins(
			"http://localhost:5173"
			)

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


#region Serilog Connfiguration

Log.Logger = new LoggerConfiguration().ReadFrom
		.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddSerilog();

#endregion



var app = builder.Build();

//  Supabase Initialization

#region Supabase Initialization

using (var scope = app.Services.CreateScope())
{
	var client = scope.ServiceProvider.GetRequiredService<Client>();
	await client.InitializeAsync();
}
#endregion

//using (var scope = app.Services.CreateScope())
//{
//	var context = scope.ServiceProvider.GetRequiredService<appDbContext>();
//	context.Database.Migrate();
//}

#region Seeders
using (var service = app.Services.CreateScope())
{
	var userManager = service.ServiceProvider.GetRequiredService<UserManager<User>>();
	var roleManager = service.ServiceProvider.GetRequiredService<RoleManager<Role>>();
	var context = service.ServiceProvider.GetRequiredService<appDbContext>();

	await CountrySeeder.SeedAsnyc(context);

	await RoleSeeder.SeedAsync(roleManager);
	await UserSeeder.SeedAsync(userManager);
	await CompanySeeder.SeedAsync(context);
	await JobSeeder.SeedAsync(context);
	await SkillSeeder.SeedAsync(context);
	await CategorySeeder.SeedAsync(context);
	await JobSkillSeeder.SeedAsync(context);
	await JobCategorySeeder.SeedAsync(context);
	await IndustrySeeder.SeedAsync(context);

}

#endregion



app.UseSwagger(options =>
{
	options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
});
app.UseSwaggerUI();


#region Localization Midlleware

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(options.Value);

#endregion

app.UseHttpsRedirection();

app.UseCors("iLinkApiCors");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHangfireDashboard(); // dashboard at /hangfire

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
