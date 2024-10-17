using Microsoft.AspNetCore.Mvc;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Application.Interfaces;

namespace SalesOrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesOrderController(ISalesOrderService salesOrderService) : ControllerBase
{

    // POST: api/salesorder
    [HttpPost]
    public async Task<IActionResult> CreateSalesOrder([FromBody] SalesOrderRequestDto salesOrderRequestDto)
    {
        if (salesOrderRequestDto == null)
        {
            return BadRequest("Sales order data is required.");
        }

        await salesOrderService.AddSalesOrderAsync(salesOrderRequestDto);

        return CreatedAtAction(nameof(GetSalesOrderById),
                               new { id = salesOrderRequestDto.SalesOrder.SalesOrderRef },
                               salesOrderRequestDto);
    }

    // GET: api/salesorder
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalesOrderDto>>> GetAllSalesOrders()
    {
        var salesOrders = await salesOrderService.GetAllSalesOrdersAsync();

        if (salesOrders == null || !salesOrders.Any())
        {
            return NoContent();
        }

        return Ok(salesOrders);
    }

    // GET: api/salesorder/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesOrderDto>> GetSalesOrderById(int id)
    {
        var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);

        if (salesOrder == null)
        {
            return NotFound($"Sales order with ID {id} not found.");
        }

        return Ok(salesOrder);
    }

    // PUT: api/salesorder/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSalesOrder(int id, [FromBody] SalesOrderDto salesOrderDto)
    {
        if (id != salesOrderDto.Id)
        {
            return BadRequest("Sales order ID mismatch.");
        }

        var existingOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
        if (existingOrder == null)
        {
            return NotFound($"Sales order with ID {id} not found.");
        }

        await salesOrderService.ModifySalesOrderAsync(salesOrderDto);

        return NoContent();
    }

    // DELETE: api/salesorder/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSalesOrder(int id)
    {
        var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
        if (salesOrder == null)
        {
            return NotFound($"Sales order with ID {id} not found.");
        }

        await salesOrderService.RemoveSalesOrderAsync(id);

        return NoContent();
    }
}
