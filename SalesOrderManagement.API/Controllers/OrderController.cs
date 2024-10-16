using Microsoft.AspNetCore.Mvc;
using SalesOrderManagement.Application.DTOs.Order;
using SalesOrderManagement.Application.Interfaces;

namespace SalesOrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await orderService.GetOrderDetailsAsync(id);
            return order == null ? (ActionResult<OrderDto>)NotFound() : (ActionResult<OrderDto>)Ok(order);
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
            await orderService.AddOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrder), new { id = orderDto.OrderRef }, orderDto);
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest("Order reference mismatch.");
            }

            await orderService.ModifyOrderAsync(orderDto);
            return NoContent();
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await orderService.RemoveOrderAsync(id);
            return NoContent();
        }
    }
}