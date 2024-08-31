namespace SalesOrderManagement.Core.Models.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }
        public DateTime OrderDate { get; set; }
        public string Currency { get; set; }
        public DateTime ShipDate { get; set; }
        public string CategoryCode { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}