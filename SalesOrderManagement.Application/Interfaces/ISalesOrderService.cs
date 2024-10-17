using SalesOrderManagement.Application.DTOs.SalesOrder;

namespace SalesOrderManagement.Application.Interfaces;

public interface ISalesOrderService
{
    /// <summary>
    /// Adds a new sales order
    /// </summary>
    /// <param name="salesOrderRequestDto"></param>
    /// <returns></returns>
    Task AddSalesOrderAsync(SalesOrderRequestDto salesOrderRequestDto);

    /// <summary>
    /// Retrieves all sales orders
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<SalesOrderDto>> GetAllSalesOrdersAsync();

    /// <summary>
    /// Retrieves a sales order by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SalesOrderDto> GetSalesOrderByIdAsync(int id);

    /// <summary>
    /// Updates an existing sales order
    /// </summary>
    /// <param name="salesOrderDto"></param>
    /// <returns></returns>
    Task ModifySalesOrderAsync(SalesOrderDto salesOrderDto);

    /// <summary>
    /// Removes a sales order by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveSalesOrderAsync(int id);
}