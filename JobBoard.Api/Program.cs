using JobBoard.Core;
using JobBoard.Core.Middleware;
using JobBoard.Core.Seeders;
using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure;
using JobBoard.Infrastructure.context;
using JobBoard.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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


#region Cors configurations

//builder.Services.AddCors(options =>
//{
//	options.AddPolicy("MyPolicy", builder =>
//	{
//		builder.WithOrigins("http://127.0.0.1:5500")
//		.AllowAnyMethod()
//		.AllowAnyHeader()
//		.AllowCredentials();

//	});
//});

#endregion

#region  Dependencies 

builder.Services.AddInfrastuctureDependencies()
	.AddServiceDependencies()
	.AddCoreDependencies()
	.AddRegistration(builder.Configuration);

#endregion


var app = builder.Build();

#region Seeders
using (var service = app.Services.CreateScope())
{
	var userManager = service.ServiceProvider.GetRequiredService<UserManager<User>>();
	var roleManager = service.ServiceProvider.GetRequiredService<RoleManager<Role>>();

	await RoleSeeder.SeedAsync(roleManager);
	await UserSeeder.SeedAsync(userManager);


}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger(options =>
	{
		options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
	});
	app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
