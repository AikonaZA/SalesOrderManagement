using AutoMapper;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Application.Interfaces;
using SalesOrderManagement.Core.Interfaces;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Application.Services;

public class SalesOrderService(IOrderRepository orderRepository, IMapper mapper) : ISalesOrderService
{
    // Handles DTO to domain model mapping and adds the sales order
    public async Task CreateSalesOrderAsync(SalesOrderRequestDto salesOrderRequestDto)
    {
        if (salesOrderRequestDto.SalesOrder == null || string.IsNullOrEmpty(salesOrderRequestDto.SalesOrder.SalesOrderRef))
            throw new ArgumentException("SalesOrder or SalesOrderRef cannot be null or empty.");

        var order = mapper.Map<Order>(salesOrderRequestDto.SalesOrder);
        await orderRepository.CreateOrderAsync(order);
    }

    // Retrieves all sales orders
    public async Task<IEnumerable<SalesOrderDto>> GetSalesOrdersAsync()
    {
        var orders = await orderRepository.GetOrdersAsync();
        return mapper.Map<IEnumerable<SalesOrderDto>>(orders);
    }

    // Retrieves a specific sales order by ID
    public async Task<SalesOrderDto> GetSalesOrderByIdAsync(int id)
    {
        var order = await orderRepository.GetOrderByIdAsync(id);
        return order == null ? throw new KeyNotFoundException($"SalesOrder with ID {id} not found.") : mapper.Map<SalesOrderDto>(order);
    }

    // Modifies an existing sales order
    public async Task UpdateSalesOrderAsync(SalesOrderDto salesOrderDto)
    {
        if (salesOrderDto == null || string.IsNullOrEmpty(salesOrderDto.SalesOrderRef))
            throw new ArgumentException("SalesOrderDto or SalesOrderRef cannot be null or empty.");

        // Ensure that the salesOrderDto.Id is not null before proceeding
        if (!salesOrderDto.Id.HasValue)
            throw new ArgumentException("SalesOrder ID cannot be null.");

        int salesOrderID = salesOrderDto.Id.Value; // Safely extract the non-null int value

        var existingOrder = await orderRepository.GetOrderByIdAsync(salesOrderID) ?? throw new KeyNotFoundException($"SalesOrder with ID {salesOrderID} not found.");

        // Map the updated details from DTO to the existing order entity
        var updatedOrder = mapper.Map(salesOrderDto, existingOrder);

        await orderRepository.UpdateOrderAsync(updatedOrder);
    }

    // Removes a sales order by ID
    public async Task DeleteSalesOrderAsync(int id)
    {
        _ = await orderRepository.GetOrderByIdAsync(id) ?? throw new KeyNotFoundException($"SalesOrder with ID {id} not found.");

        // Directly use the provided id for deletion
        await orderRepository.DeleteOrderAsync(id);
    }
}