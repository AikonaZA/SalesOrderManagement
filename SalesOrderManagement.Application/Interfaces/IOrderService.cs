using SalesOrderManagement.Application.DTOs.Order;
using SalesOrderManagement.Application.DTOs.SalesOrder;

namespace SalesOrderManagement.Application.Interfaces;

[Obsolete("Order Service is unused and SalesOrder Service will be used going forward")]
public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

    Task<OrderDto> GetOrderDetailsAsync(int id);

    Task AddOrderAsync(OrderDto order);

    Task ModifyOrderAsync(OrderDto order);

    Task RemoveOrderAsync(int id);

    Task AddSalesOrderAsync(SalesOrderRequestDto salesOrderRequestDto);
}