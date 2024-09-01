using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesOrderManagement.Core.Interfaces;
using SalesOrderManagement.Infrastructure.Data;
using SalesOrderManagement.Infrastructure.Repositories;

namespace SalesOrderManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string connectionString)
        {
            // Register DbContext with SQLite connection string
            services.AddDbContext<SalesOrderDbContext>(options =>
                options.UseSqlite(connectionString));

            // Register the repository
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}