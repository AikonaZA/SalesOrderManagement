using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Application.Interfaces;

namespace SalesOrderManagement.API.Endpoints.SalesOrder;

public static class SalesOrderEndpoints
{
    public static void MapSalesOrderEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/salesorder", async (ISalesOrderService salesOrderService, SalesOrderRequestDto salesOrderRequestDto) =>
        {
            if (salesOrderRequestDto == null)
                return Results.BadRequest("Sales order data is required.");

            await salesOrderService.AddSalesOrderAsync(salesOrderRequestDto);

            return Results.Created($"/api/salesorder/{salesOrderRequestDto.SalesOrder.SalesOrderRef}", salesOrderRequestDto);
        });

        endpoints.MapGet("/api/salesorder", async (ISalesOrderService salesOrderService) =>
        {
            var salesOrders = await salesOrderService.GetAllSalesOrdersAsync();

            return salesOrders == null || !salesOrders.Any() ? Results.NoContent() : Results.Ok(salesOrders);
        });

        endpoints.MapGet("/api/salesorder/{id:int}", async (ISalesOrderService salesOrderService, int id) =>
        {
            var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);

            return salesOrder == null
                ? Results.NotFound($"Sales order with ID {id} not found.")
                : Results.Ok(salesOrder);
        });

        endpoints.MapPut("/api/salesorder/{id:int}", async (ISalesOrderService salesOrderService, int id, SalesOrderDto salesOrderDto) =>
        {
            if (id != salesOrderDto.Id)
                return Results.BadRequest("Sales order ID mismatch.");

            var existingOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
            if (existingOrder == null)
                return Results.NotFound($"Sales order with ID {id} not found.");

            await salesOrderService.ModifySalesOrderAsync(salesOrderDto);

            return Results.NoContent();
        });

        endpoints.MapDelete("/api/salesorder/{id:int}", async (ISalesOrderService salesOrderService, int id) =>
        {
            var salesOrder = await salesOrderService.GetSalesOrderByIdAsync(id);
            if (salesOrder == null)
                return Results.NotFound($"Sales order with ID {id} not found.");

            await salesOrderService.RemoveSalesOrderAsync(id);

            return Results.NoContent();
        });
    }
}