using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order> GetOrderByIdAsync(int id);

        Task CreateOrderAsync(Order order);

        Task UpdateOrderAsync(Order order);

        Task DeleteOrderAsync(int id);
    }
}