using Microsoft.AspNetCore.Components;
using Radzen;
using SalesOrderManagement.Application.DTOs.SalesOrder;

namespace SalesOrderManagement.Client.Components.Pages;

public partial class OrderDetailsPage
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    public HttpClient Http { get; set; }

    private SalesOrderDto order;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            order = await Http.GetFromJsonAsync<SalesOrderDto>($"api/salesorder/{Id}");
        }
        catch (Exception ex)
        {
            notification.Notify(NotificationSeverity.Error, "Error", $"Failed to load order details: {ex.Message}");
        }
    }
}
