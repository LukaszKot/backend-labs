using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnTheTrail.IdentityService.Api.Database;
using OnTheTrail.IdentityService.Api.Exceptions;
using OnTheTrail.IdentityService.Api.Repositories;
using OnTheTrail.IdentityService.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});
builder.Services.AddDbContext<IdentityDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetValue<string>("dbConnectionString"),
        optionsBuilder => optionsBuilder.EnableRetryOnFailure()));
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwtBearerOptions =>
    {
        jwtBearerOptions.RequireHttpsMetadata = false;
        jwtBearerOptions.SaveToken = true;
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("secretKey"))),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
            ValidAudience = "audience",
            ValidIssuer = "issuer",
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

app.UseAuthentication();

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