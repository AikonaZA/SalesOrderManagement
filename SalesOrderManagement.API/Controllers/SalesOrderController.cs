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
        await salesOrderService.AddSalesOrderAsync(salesOrderRequestDto);
        return CreatedAtAction(nameof(GetSalesOrderById), new { id = salesOrderRequestDto.SalesOrder.SalesOrderRef }, salesOrderRequestDto);
    }

    // GET: api/salesorder
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalesOrderDto>>> GetAllSalesOrders()
    {
        var salesOrders = await salesOrderService.GetAllSalesOrdersAsync();
        return Ok(salesOrders);
    }

    // GET: api/salesorder/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesOrderDto>> GetSalesOrderById(int id)
    {
        var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
        return salesOrder == null ? (ActionResult<SalesOrderDto>)NotFound() : (ActionResult<SalesOrderDto>)Ok(salesOrder);
    }
}