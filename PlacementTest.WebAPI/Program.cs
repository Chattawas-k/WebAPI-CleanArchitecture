using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PlacementTest.Application;
using PlacementTest.Persistence;
using PlacementTest.Persistence.Context;
using PlacementTest.WebAPI.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    var jwtKey = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
})
.AddJwtBearer("AzureAd", options =>
{
    options.Authority = $"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}/v2.0";
    options.Audience = builder.Configuration["AzureAd:ClientId"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true
    };
});
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
dataContext?.Database.EnsureCreated();

app.UseSwagger();
app.UseSwaggerUI();
app.UseErrorHandler();
app.UseCors();
app.MapControllers();
app.Run();