using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlacementTest.Application.Common.Services;
using PlacementTest.Application.Repository;
using PlacementTest.Application.Repository.TestTakersRepository;
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
            //Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITestTakerRepository, TestTakerRepository>();
            //Services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
