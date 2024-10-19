using Carter;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Application.Interfaces;

namespace SalesOrderManagement.API.Endpoints.SalesOrder;

public class SalesOrderEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/salesorder");

        group.MapPost("", CreateSalesOrder).WithName(nameof(CreateSalesOrder));

        group.MapGet("", GetSalesOrders).WithName(nameof(GetSalesOrders));

        group.MapGet("{id}", GetSalesOrder).WithName(nameof(GetSalesOrder));

        group.MapPut("{id}", UpdateSalesOrder).WithName(nameof(UpdateSalesOrder));

        group.MapDelete("{id}", DeleteSalesOrder).WithName(nameof(DeleteSalesOrder));
    }

    public static async Task<IResult> CreateSalesOrder(ISalesOrderService salesOrderService, SalesOrderRequestDto salesOrderRequestDto)
    {
        if (salesOrderRequestDto == null)
            return Results.BadRequest("Sales order data is required.");

        await salesOrderService.CreateSalesOrderAsync(salesOrderRequestDto);

        return Results.Created($"/api/salesorder/{salesOrderRequestDto.SalesOrder.SalesOrderRef}", salesOrderRequestDto);
    }

    public static async Task<IResult> GetSalesOrders(ISalesOrderService salesOrderService)
    {
        var salesOrders = await salesOrderService.GetSalesOrdersAsync();

        return salesOrders == null || !salesOrders.Any() 
            ? Results.NoContent() 
            : Results.Ok(salesOrders);
    }

    public static async Task<IResult> GetSalesOrder(ISalesOrderService salesOrderService, int id)
    {
        var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);

        return salesOrder == null
            ? Results.NotFound($"Sales order with ID {id} not found.")
            : Results.Ok(salesOrder);
    }

    public static async Task<IResult> UpdateSalesOrder(ISalesOrderService salesOrderService, int id, SalesOrderDto salesOrderDto)
    {
        if (id != salesOrderDto.Id)
            return Results.BadRequest("Sales order ID mismatch.");

        var existingOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
        if (existingOrder == null)
            return Results.NotFound($"Sales order with ID {id} not found.");

        await salesOrderService.UpdateSalesOrderAsync(salesOrderDto);

        return Results.NoContent();
    }

    public static async Task<IResult> DeleteSalesOrder(ISalesOrderService salesOrderService, int id)
    {
        var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
        if (salesOrder == null)
            return Results.NotFound($"Sales order with ID {id} not found.");

        await salesOrderService.DeleteSalesOrderAsync(id);

        return Results.NoContent();
    }
}