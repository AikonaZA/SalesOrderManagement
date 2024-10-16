using Radzen;
using SalesOrderManagement.Application.DTOs.Order;

namespace SalesOrderManagement.Client.Components.Pages;

public partial class OrderForm
{
    private readonly OrderDto newOrder = new();

    private async Task HandleValidSubmit()
    {
        try
        {
            await Http.PostAsJsonAsync("api/order", newOrder);
            notification.Notify(NotificationSeverity.Success, "Order Created", "Order has been successfully created.");
        }
        catch (Exception ex)
        {
            notification.Notify(NotificationSeverity.Error, "Error", $"Order creation failed: {ex.Message}");
        }
    }
}