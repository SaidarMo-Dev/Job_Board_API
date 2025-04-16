using JobBoard.Core;
using JobBoard.Core.Middleware;
using JobBoard.Infrastructure;
using JobBoard.Infrastructure.Data;
using JobBoard.Service;
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

builder.Services.AddCors(options =>
{
	options.AddPolicy("MyPolicy", builder =>
	{
		builder.WithOrigins("http://127.0.0.1:5500")
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
	.AddRegistration();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
