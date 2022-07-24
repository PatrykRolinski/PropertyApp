using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyApp.Infrastructure
{
    public static class InfrastructureServices
    {
        public static  IServiceCollection AddPropertyAppInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<PropertyAppContext>(options => options.UseSqlServer(config.GetConnectionString("PropertyAppDbConnection")));
            services.AddScoped<SeedData>();
            return services;
        }
           
    }
}
