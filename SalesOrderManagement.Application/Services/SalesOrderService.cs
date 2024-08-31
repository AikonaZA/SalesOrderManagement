using AutoMapper;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Application.Interfaces;
using SalesOrderManagement.Core.Interfaces;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Application.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public SalesOrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        // Handles DTO to domain model mapping and adds the order
        public async Task AddSalesOrderAsync(SalesOrderRequestDto salesOrderRequestDto)
        {
            // Check if the sales order or its reference is null or empty
            if (salesOrderRequestDto.SalesOrder == null || string.IsNullOrEmpty(salesOrderRequestDto.SalesOrder.SalesOrderRef))
            {
                throw new ArgumentException("SalesOrder or OrderRef cannot be null or empty.");
            }

            // Map DTO to the domain model and create the order
            var order = _mapper.Map<Order>(salesOrderRequestDto.SalesOrder);
            await _orderRepository.CreateOrderAsync(order);
        }

        // Retrieve all sales orders
        public async Task<IEnumerable<SalesOrderDto>> GetAllSalesOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return _mapper.Map<IEnumerable<SalesOrderDto>>(orders);
        }

        // Retrieve a specific sales order by ID
        public async Task<SalesOrderDto> GetSalesOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return _mapper.Map<SalesOrderDto>(order);
        }
    }
}