using Radzen;
using SalesOrderManagement.Application.DTOs.SalesOrder;

namespace SalesOrderManagement.Client.Components.Pages;

public partial class OrderForm
{
    private readonly SalesOrderDto newSalesOrder = new();

    private async Task HandleValidSubmit()
    {
        try
        {
            await Http.PostAsJsonAsync("api/salesorder", newSalesOrder);
            notification.Notify(NotificationSeverity.Success, "Order Created", "Sales order has been successfully created.");
        }
        catch (Exception ex)
        {
            notification.Notify(NotificationSeverity.Error, "Error", $"Sales order creation failed: {ex.Message}");
        }
    }
}