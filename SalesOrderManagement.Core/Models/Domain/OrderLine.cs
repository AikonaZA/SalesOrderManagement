namespace SalesOrderManagement.Core.Models.Domain
{
    public class OrderLine
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; } // Foreign key to Order
    }
}