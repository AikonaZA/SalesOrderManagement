namespace SalesOrderManagement.Application.DTOs.Order;

public class OrderLineDto
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}