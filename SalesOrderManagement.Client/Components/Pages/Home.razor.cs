using Microsoft.AspNetCore.Components;

namespace SalesOrderManagement.Client.Components.Pages;

public class HomeBase : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected void NavigateToOverview()
    {
        NavigationManager.NavigateTo("/orders/overview");
    }
}
