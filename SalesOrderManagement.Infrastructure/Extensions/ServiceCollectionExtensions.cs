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
            // Register DbContext with connection string
            services.AddDbContext<SalesOrderDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register repositories
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
