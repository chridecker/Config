using ConfigApi.Extensions;
using ConfigApi.Services;
using ConfigApi.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DataAccess;
using DataAccess.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Auth",
        Name = "Authorization",
        BearerFormat = JwtConstants.TokenType,
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = JwtBearerDefaults.AuthenticationScheme,
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        } });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

builder.Services.AddTransient<ISettingAppService, SettingAppService>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

builder.Services.AddDbContext<Context>(opt =>
{
    opt.UseSqlite($"Data Source={(Path.Combine(Directory.GetCurrentDirectory(), "database.db"))}");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Issuer)}"],
        ValidAudience = builder.Configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Audience)}"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Key)}"]))
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

var services = app.MapGroup("/services").WithTags("Services");
services.MapPost("createtoken", async (Guid id, Context context, IJwtTokenService tokenAppService) =>
{
    var token = await tokenAppService.BuildToken(id);
    return string.IsNullOrWhiteSpace(token) ? Results.Unauthorized() : Results.Ok(new TokenDto { Token = token });
}).WithName("createtoken");

services.MapGet("settings/{version}", async (Version version, ISettingAppService settinService, IJwtTokenService tokenAppService, HttpContext httpContext, CancellationToken cancellationToken = default) =>
{
    var token = await httpContext.GetJWTTokenAsync();
    var service = await tokenAppService.ValidateToken(token, cancellationToken);
    var setting = await settinService.LoadServiceSetting(service.Id, version, cancellationToken);
    return setting.Value;
})
.RequireAuthorization()
.WithName("settings");

app.Run();
