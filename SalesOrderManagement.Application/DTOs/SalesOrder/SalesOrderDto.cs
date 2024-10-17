namespace SalesOrderManagement.Application.DTOs.SalesOrder;

public class SalesOrderDto
{
    public int? Id { get; set; }
    public string SalesOrderRef { get; set; }
    public DateTime OrderDate { get; set; }
    public string Currency { get; set; }
    public DateTime ShipDate { get; set; }
    public string CategoryCode { get; set; }
    public List<SalesOrderLineDto> OrderLines { get; set; }
}