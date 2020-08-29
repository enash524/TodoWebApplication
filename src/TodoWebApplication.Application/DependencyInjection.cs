using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TodoWebApplication.Application
{
    /// <summary>
    /// Provides methods for adding TodoWebApplication.Application to the dependency injection service collection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds dependency injection for the TodoWebApplication.Application project to the services collection.
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the TodoWebApplication.Application to.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional calls can be chained.</returns>

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
