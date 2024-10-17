namespace SalesOrderManagement.Application.DTOs.Order;

[Obsolete("Order DTO to be replaced with SalesOrder DTO")]
public class OrderLineDto
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}