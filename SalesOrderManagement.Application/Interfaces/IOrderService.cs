using SalesOrderManagement.Application.DTOs;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

        Task<OrderDto> GetOrderDetailsAsync(int id);

        Task AddOrderAsync(OrderDto order);

        Task ModifyOrderAsync(OrderDto order);

        Task RemoveOrderAsync(int id);
    }
}