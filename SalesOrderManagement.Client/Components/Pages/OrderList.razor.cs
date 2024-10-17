using Radzen;
using SalesOrderManagement.Application.DTOs.SalesOrder;

namespace SalesOrderManagement.Client.Components.Pages;

public partial class OrderList
{
    private List<SalesOrderDto> salesOrders = [];

    protected override async Task OnInitializedAsync()
    {
        await LoadSalesOrdersAsync();
    }

    private async Task LoadSalesOrdersAsync()
    {
        try
        {
            salesOrders = await Http.GetFromJsonAsync<List<SalesOrderDto>>("api/SalesOrder");
        }
        catch (Exception ex)
        {
            notification.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = $"Failed to load sales orders: {ex.Message}",
                Duration = 4000
            });
        }
    }

    private void EditSalesOrder(SalesOrderDto salesOrder)
    {
        // Implement the navigation logic to the edit page (e.g., NavigationManager.NavigateTo($"orders/edit/{salesOrder.Id}"))
    }

    private async Task DeleteSalesOrder(int? id)
    {
        try
        {
            await Http.DeleteAsync($"api/salesorder/{id}");
            await LoadSalesOrdersAsync();
            notification.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Deleted",
                Detail = "Sales order deleted successfully.",
                Duration = 4000
            });
        }
        catch (Exception ex)
        {
            notification.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = $"Failed to delete sales order: {ex.Message}",
                Duration = 4000
            });
        }
    }
}