using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface IHistoriesService
{
    Task<IReadOnlyList<HistoryDTO>> GetAllHistoriesAsync();
    Task<IReadOnlyList<HistoryDTO>> GetByStockIdAsync(int stockId);
    Task<IReadOnlyList<HistoryDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}