using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SalesOrderManagement.Core.Interfaces;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Orders";
                var orders = await connection.QueryAsync<Order>(sql);
                return orders;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Orders WHERE Id = @Id";
                var order = await connection.QuerySingleOrDefaultAsync<Order>(sql, new { Id = id });
                return order;
            }
        }

        public async Task CreateOrderAsync(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Orders (OrderRef, OrderDate, Currency, ShipDate, CategoryCode) 
                               VALUES (@OrderRef, @OrderDate, @Currency, @ShipDate, @CategoryCode)";
                await connection.ExecuteAsync(sql, order);
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Orders 
                               SET OrderRef = @OrderRef, OrderDate = @OrderDate, Currency = @Currency, 
                                   ShipDate = @ShipDate, CategoryCode = @CategoryCode 
                               WHERE Id = @Id";
                await connection.ExecuteAsync(sql, order);
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Orders WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
