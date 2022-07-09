using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OnTheTrail.IdentityService.Api.Database;
using OnTheTrail.IdentityService.Api.Exceptions;
using OnTheTrail.IdentityService.Api.Repositories;
using OnTheTrail.IdentityService.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IdentityDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetValue<string>("dbConnectionString")));
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IdentityDbContext>().Database.Migrate();
}

app.UseExceptionHandler(applicationBuilder =>
    applicationBuilder.Run(async context =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is AppException appException)
        {
            context.Response.StatusCode = (int) appException.StatusCode;
            await context.Response.WriteAsJsonAsync(new { appException.Message });
        }
    }));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();