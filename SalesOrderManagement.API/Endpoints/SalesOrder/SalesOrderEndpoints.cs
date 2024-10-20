using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Application.Interfaces;

namespace SalesOrderManagement.API.Endpoints.SalesOrder;

public class SalesOrderEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/salesorder")
            .WithOpenApi();

        group.MapPost("", CreateSalesOrder).WithName(nameof(CreateSalesOrder));

        group.MapGet("", GetSalesOrders).WithName(nameof(GetSalesOrders));

        group.MapGet("{id}", GetSalesOrder).WithName(nameof(GetSalesOrder));

        group.MapPut("{id}", UpdateSalesOrder).WithName(nameof(UpdateSalesOrder));

        group.MapDelete("{id}", DeleteSalesOrder).WithName(nameof(DeleteSalesOrder));
    }

    public static async Task<Results<Created<SalesOrderRequestDto>, BadRequest<string>>> CreateSalesOrder(ISalesOrderService salesOrderService, SalesOrderRequestDto salesOrderRequestDto)
    {
        if (salesOrderRequestDto == null)
            return TypedResults.BadRequest("Sales order data is required.");

        await salesOrderService.CreateSalesOrderAsync(salesOrderRequestDto);

        return TypedResults.Created($"/api/salesorder/{salesOrderRequestDto.SalesOrder.SalesOrderRef}", salesOrderRequestDto);
    }

    public static async Task<Results<Ok<IEnumerable<SalesOrderDto>>,NoContent>> GetSalesOrders(ISalesOrderService salesOrderService)
    {
        var salesOrders = await salesOrderService.GetSalesOrdersAsync();

        return salesOrders == null || !salesOrders.Any() 
            ? TypedResults.NoContent() 
            : TypedResults.Ok(salesOrders);
    }

    public static async Task<Results<Ok<SalesOrderDto>, NotFound<string>>> GetSalesOrder(ISalesOrderService salesOrderService, int id)
    {
        var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);

        return salesOrder == null
            ? TypedResults.NotFound($"Sales order with ID {id} not found.")
            : TypedResults.Ok(salesOrder);
    }

    public static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> UpdateSalesOrder(ISalesOrderService salesOrderService, int id, SalesOrderDto salesOrderDto)
    {
        if (id != salesOrderDto.Id)
            return TypedResults.BadRequest("Sales order ID mismatch.");

        var existingOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
        if (existingOrder == null)
            return TypedResults.NotFound($"Sales order with ID {id} not found.");

        await salesOrderService.UpdateSalesOrderAsync(salesOrderDto);

        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent,NotFound<string>>> DeleteSalesOrder(ISalesOrderService salesOrderService, int id)
    {
        var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
        if (salesOrder == null)
            return TypedResults.NotFound($"Sales order with ID {id} not found.");

        await salesOrderService.DeleteSalesOrderAsync(id);

        return TypedResults.NoContent();
    }
}