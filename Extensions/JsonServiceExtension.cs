using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DashboardMVC.Extensions
{
    public static class JsonServiceExtension
    {
        public static IServiceCollection AddJsonService(this IServiceCollection services, IConfiguration configuration)
        {

            // services.AddControllers()
            //     .AddNewtonsoftJson(options =>
            //     {
            //         options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //     }
            // );
            services
               .AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                   options.JsonSerializerOptions.IgnoreNullValues = true;
               });
            return services;
        }
    }
}