using Microsoft.Extensions.DependencyInjection;

namespace MyFarm.Log
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> for configuring services.
    /// </summary>
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, AppLogger>();

            return services;
        }
    }
}
