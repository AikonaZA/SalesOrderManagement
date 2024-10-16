using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using SalesOrderManagement.Core.Interfaces;
using SalesOrderManagement.Core.Models.Domain;
using System.Data;

namespace SalesOrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private readonly string _connectionString;
        private readonly SqliteConnection _connection; // Manage the SQLite connection instance
        private bool _disposed = false; // Track whether the object has been disposed

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = CreateConnection(); // Initialize the connection
        }

        // Creates and returns a new SQLite connection
        private SqliteConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        // Ensure connection is open and ready to use
        private async Task EnsureConnectionOpenAsync()
        {
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
        }

        // Retrieve all orders
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            await EnsureConnectionOpenAsync();
            string sql = "SELECT * FROM Orders";
            var orders = await _connection.QueryAsync<Order>(sql);
            return orders;
        }

        // Retrieve an order by its ID
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            await EnsureConnectionOpenAsync();
            string sql = "SELECT * FROM Orders WHERE Id = @Id";
            var order = await _connection.QuerySingleOrDefaultAsync<Order>(sql, new { Id = id });
            return order;
        }

        // Create a new order
        public async Task CreateOrderAsync(Order order)
        {
            await EnsureConnectionOpenAsync();
            string sql = @"INSERT INTO Orders (OrderRef, OrderDate, Currency, ShipDate, CategoryCode)
                           VALUES (@OrderRef, @OrderDate, @Currency, @ShipDate, @CategoryCode)";
            await _connection.ExecuteAsync(sql, order);
        }

        // Update an existing order
        public async Task UpdateOrderAsync(Order order)
        {
            await EnsureConnectionOpenAsync();
            string sql = @"UPDATE Orders
                           SET OrderRef = @OrderRef, OrderDate = @OrderDate, Currency = @Currency,
                               ShipDate = @ShipDate, CategoryCode = @CategoryCode
                           WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, order);
        }

        // Delete an order by its ID
        public async Task DeleteOrderAsync(int id)
        {
            await EnsureConnectionOpenAsync();
            string sql = "DELETE FROM Orders WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        // Implement IDisposable to properly release the connection
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose of managed resources
                    _connection?.Dispose();
                }

                // Mark as disposed
                _disposed = true;
            }
        }

        // Public dispose method
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Suppress finalization to optimize garbage collection
        }

        // Destructor for safety, in case Dispose is not called
        ~OrderRepository()
        {
            Dispose(false);
        }
    }
}