using DashboardMVC.Data;
using DashboardMVC.Data.Services;
using DashboardMVC.Helpers;
using DashboardMVC.Interfaces;
using DashboardMVC.Interfaces.Services;
using DashboardMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DashboardMVC.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationGroupService, ApplicationGroupService>();
            services.AddScoped<IApplicationUserGroupRepository, IApplicationUserGroupRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<ApplicationDbContext>(options =>
                 options
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
            return services;
        }
    }
}