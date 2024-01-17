using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFarm.Data;
using MyFarm.WebApi.AutoMapper;
using MyFarm.WebApi.Services;

namespace MyFarm.WebApi.Extensions
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> for configuring services.
    /// </summary>
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services) 
        {
            var dbName = $"{nameof(MyFarm)}InMemoryDb";
            services.AddDbContext<FarmDbContext>(options => options.UseInMemoryDatabase(dbName));

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services) 
        {
            services
                .AddSingleton(new MapperConfiguration(mc => 
                { 
                    mc.AddProfile(new MappingProfile()); 
                }).CreateMapper());
            
            return services;
        }

        public static IServiceCollection AddWebApiServices(this IServiceCollection services) 
        {
            services.AddTransient<AppInitializer>();

            return services;
        }

        public static IServiceCollection AddFarmCorsPolicy(this IServiceCollection services) 
        {
            services.AddCors(o => o.AddPolicy("FarmPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            return services;
        }
    }
}
