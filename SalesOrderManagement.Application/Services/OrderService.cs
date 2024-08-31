using AutoMapper;
using SalesOrderManagement.Application.DTOs.Order;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Application.Interfaces;
using SalesOrderManagement.Core.Interfaces;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        // Returns DTOs by mapping domain models to DTOs
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        // Returns a single DTO for order details
        public async Task<OrderDto> GetOrderDetailsAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        // Handles DTO to domain model mapping and adds the order
        public async Task AddOrderAsync(OrderDto orderDto)
        {
            if (string.IsNullOrEmpty(orderDto.OrderRef))
            {
                throw new ArgumentException("Order reference cannot be empty");
            }

            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.CreateOrderAsync(order);
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
            var order = _mapper.Map<Order>(salesOrderRequestDto.SalesOrder);
            await _orderRepository.CreateOrderAsync(order);
        }

        // Handles modification of the order using the DTO
        public async Task ModifyOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            var existingOrder = await _orderRepository.GetOrderByIdAsync(order.Id);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            await _orderRepository.UpdateOrderAsync(order);
        }

        // Removes the order based on its ID
        public async Task RemoveOrderAsync(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
        }
    }
}