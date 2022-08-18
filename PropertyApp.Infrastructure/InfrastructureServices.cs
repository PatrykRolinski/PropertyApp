using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Infrastructure.Repositories;

namespace PropertyApp.Infrastructure
{
    public static class InfrastructureServices
    {
        public static  IServiceCollection AddPropertyAppInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<PropertyAppContext>(
                options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(config.GetConnectionString("PropertyAppDbConnection")));
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IMessageRepository, MesssageRepository>();
            services.AddScoped<SeedData>();
            return services;
        }
           
    }
}
