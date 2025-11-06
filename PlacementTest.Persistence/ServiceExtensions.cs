using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlacementTest.Application.Common.Services;
using PlacementTest.Application.Repository;
using PlacementTest.Application.Repository.TestTakersRepository;
using PlacementTest.Domain.Entities;
using PlacementTest.Persistence.Context;
using PlacementTest.Persistence.Repository;
using PlacementTest.Persistence.Repository.TestTakersRepository;
using PlacementTest.Persistence.Services;

namespace PlacementTest.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("ApplicationDbContext");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            // Configure ASP.NET Core Identity
            services.AddIdentity<User, Role>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add IHttpClientFactory
            services.AddHttpClient();

            //Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITestTakerRepository, TestTakerRepository>();
            //Services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Register authentication/login related services
            services.AddScoped<ILoginService, LoginService>();
        }
    }
}
