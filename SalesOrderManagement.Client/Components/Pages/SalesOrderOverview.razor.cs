using Microsoft.AspNetCore.Components;
using Radzen;
using SalesOrderManagement.Application.DTOs.SalesOrder;

namespace SalesOrderManagement.Client.Components.Pages;

public partial class SalesOrderOverview
{
    private List<SalesOrderDto> orderHeaders = [];
    private List<SalesOrderLineDto> orderLines = [];
    private bool showOrderLines = false;

    private string searchOrderRef;
    private string searchOrderType;
    private DateTime? searchStartDate;
    private DateTime? searchEndDate;

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadOrderHeadersAsync();
    }

    private async Task LoadOrderHeadersAsync()
    {
        try
        {
            orderHeaders = await Http.GetFromJsonAsync<List<SalesOrderDto>>("api/salesorder");
        }
        catch (Exception ex)
        {
            notification.Notify(NotificationSeverity.Error, "Error", $"Failed to load order headers: {ex.Message}");
        }
    }

    private void OnOrderSelected(SalesOrderDto selectedOrder)
    {
        orderLines = selectedOrder.OrderLines;
        showOrderLines = true;
    }

    private async Task SearchOrders()
    {
        try
        {
            var searchUri = $"api/salesorder/search?orderRef={searchOrderRef}&orderType={searchOrderType}&startDate={searchStartDate}&endDate={searchEndDate}";
            orderHeaders = await Http.GetFromJsonAsync<List<SalesOrderDto>>(searchUri);
        }
        catch (Exception ex)
        {
            notification.Notify(NotificationSeverity.Error, "Error", $"Search failed: {ex.Message}");
        }
    }

    private void EditOrderHeader(SalesOrderDto orderHeader)
    {
        // Implement inline, popup, or navigate to detailed edit page logic
    }

    private void DeleteOrderHeader(int? id)
    {
        // Implement delete logic
    }

    private void EditOrderLine(SalesOrderLineDto orderLine)
    {
        // Implement inline or popup edit logic
    }

    private void DeleteOrderLine(SalesOrderLineDto orderLine)
    {
        orderLines.Remove(orderLine);
    }

    private void NavigateToOrderDetails(int? id)
    {
        NavigationManager.NavigateTo($"/orders/details/{id}");
    }
}
