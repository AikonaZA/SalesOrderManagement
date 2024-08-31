using SalesOrderManagement.Application.DTOs.SalesOrder;

namespace SalesOrderManagement.Application.Interfaces
{
    public interface ISalesOrderService
    {
        Task AddSalesOrderAsync(SalesOrderRequestDto salesOrderRequestDto);

        Task<IEnumerable<SalesOrderDto>> GetAllSalesOrdersAsync();

        Task<SalesOrderDto> GetSalesOrderByIdAsync(int id);
    }
}