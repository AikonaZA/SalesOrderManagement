using Radzen;
using SalesOrderManagement.Application.DTOs.SalesOrder;

namespace SalesOrderManagement.Client.Components.Pages;

public partial class OrderManagementPage
{
    private readonly SalesOrderDto salesOrder = new()
    {
        OrderLines = []
    };

    private List<SalesOrderLineDto> OrderLines => salesOrder.OrderLines;

    private async Task HandleValidSubmit()
    {
        try
        {
            var request = new SalesOrderRequestDto { SalesOrder = salesOrder };
            await Http.PostAsJsonAsync("api/salesorder", request);
            notification.Notify(NotificationSeverity.Success, "Order Submitted", "Sales order has been successfully submitted.");
        }
        catch (Exception ex)
        {
            notification.Notify(NotificationSeverity.Error, "Error", $"Order submission failed: {ex.Message}");
        }
    }

    private void AddOrderLine()
    {
        OrderLines.Add(new SalesOrderLineDto());
    }

    private void EditOrderLine(SalesOrderLineDto orderLine)
    {
        // Logic for editing an existing order line
    }

    private void DeleteOrderLine(SalesOrderLineDto orderLine)
    {
        OrderLines.Remove(orderLine);
    }
}
