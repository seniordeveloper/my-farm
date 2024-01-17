using Microsoft.Extensions.DependencyInjection;
using MyFarm.Contracts.Services;
using MyFarm.Core.Services;

namespace MyFarm.Core
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> for configuring services.
    /// </summary>
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services) 
        {
            services
                .AddTransientServices();

            return services;
        }

        private static IServiceCollection AddTransientServices(this IServiceCollection services)
        {
            services
                .AddTransient<IAnimalManager, AnimalManager>();

            return services;
        }
    }
}
