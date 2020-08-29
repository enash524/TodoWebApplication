using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoWebApplication.Data.Interfaces;
using TodoWebApplication.Data.Repositories;

namespace TodoWebApplication.Data
{
    /// <summary>
    /// Provides methods for adding TodoWebApplication.Data to the dependency injection service collection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds dependency injection for the TodoWebApplication.Data project to the services collection.
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the TodoWebApplication.Data to.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            return services;
        }
    }
}
