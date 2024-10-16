using AutoMapper;
using SalesOrderManagement.Application.DTOs.Order;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Application.Interfaces;
using SalesOrderManagement.Core.Interfaces;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Application.Services
{
    public class OrderService(IOrderRepository orderRepository, IMapper mapper) : IOrderService
    {

        // Returns DTOs by mapping domain models to DTOs
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.GetOrdersAsync();
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        // Returns a single DTO for order details
        public async Task<OrderDto> GetOrderDetailsAsync(int id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);
            return mapper.Map<OrderDto>(order);
        }

        // Handles DTO to domain model mapping and adds the order
        public async Task AddOrderAsync(OrderDto orderDto)
        {
            if (string.IsNullOrEmpty(orderDto.OrderRef))
            {
                throw new ArgumentException("Order reference cannot be empty");
            }

            var order = mapper.Map<Order>(orderDto);
            await orderRepository.CreateOrderAsync(order);
        }

        // Handles DTO to domain model mapping and adds the order
        public async Task AddSalesOrderAsync(SalesOrderRequestDto salesOrderRequestDto)
        {
            // Check if the sales order or its reference is null or empty
            if (salesOrderRequestDto.SalesOrder == null)
            {
                throw new ArgumentException("SalesOrder cannot be null.");
            }

            // Check if the SalesOrderRef is null or empty
            if (string.IsNullOrEmpty(salesOrderRequestDto.SalesOrder.SalesOrderRef))
            {
                throw new ArgumentException("Order reference cannot be empty.");
            }

            // Map DTO to the domain model and create the order
            var order = mapper.Map<Order>(salesOrderRequestDto.SalesOrder);
            await orderRepository.CreateOrderAsync(order);
        }

        // Handles modification of the order using the DTO
        public async Task ModifyOrderAsync(OrderDto orderDto)
        {
            var order = mapper.Map<Order>(orderDto);
            var existingOrder = await orderRepository.GetOrderByIdAsync(order.Id);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            await orderRepository.UpdateOrderAsync(order);
        }

        // Removes the order based on its ID
        public async Task RemoveOrderAsync(int id)
        {
            await orderRepository.DeleteOrderAsync(id);
        }
    }
}