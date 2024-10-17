using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace SalesOrderManagement.Client.Components.Pages;

public class ErrorBase : ComponentBase
{
    [CascadingParameter]
    public HttpContext? HttpContext { get; set; }

    public string? RequestId { get; set; }

    protected bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    protected override void OnInitialized()
    {
        RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
    }
}
