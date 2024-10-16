using Radzen;
using SalesOrderManagement.Application.DTOs.Order;

namespace SalesOrderManagement.Client.Components.Pages;

public partial class OrderList
{
    private List<OrderDto> orders = [];

    protected override async Task OnInitializedAsync()
    {
        await LoadOrdersAsync();
    }

    private async Task LoadOrdersAsync()
    {
        try
        {
            orders = await Http.GetFromJsonAsync<List<OrderDto>>("api/Order");
        }
        catch (Exception ex)
        {
            notification.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = $"Failed to load orders: {ex.Message}",
                Duration = 4000
            });
        }
    }

    private void EditOrder(OrderDto order)
    {
        // Implement the navigation logic to the edit page
    }

    private async Task DeleteOrder(int id)
    {
        try
        {
            await Http.DeleteAsync($"api/order/{id}");
            await LoadOrdersAsync();
            notification.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Deleted",
                Detail = "Order deleted successfully.",
                Duration = 4000
            });
        }
        catch (Exception ex)
        {
            notification.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = $"Failed to delete order: {ex.Message}",
                Duration = 4000
            });
        }
    }
}