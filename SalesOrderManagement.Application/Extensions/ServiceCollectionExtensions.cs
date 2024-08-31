using Microsoft.Extensions.DependencyInjection;
using SalesOrderManagement.Application.Interfaces;
using SalesOrderManagement.Application.Services;

namespace SalesOrderManagement.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Extension method to add application services, repositories, and other configurations
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Register AutoMapper with the mapping profiles
            services.AddAutoMapper(typeof(MappingProfile));

            // Register the application services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();

            // Add any other services, e.g., logging, caching, etc.

            return services;
        }
    }
}