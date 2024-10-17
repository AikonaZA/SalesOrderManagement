namespace SalesOrderManagement.Application.DTOs.Order;
[Obsolete("Order DTO to be replaced with SalesOrder DTO")]
public class OrderDto
{
    public int Id { get; set; }
    public string OrderRef { get; set; }
    public DateTime OrderDate { get; set; }
    public string Currency { get; set; }
    public DateTime ShipDate { get; set; }
    public string CategoryCode { get; set; }
    public List<OrderLineDto> OrderLines { get; set; } = [];
}