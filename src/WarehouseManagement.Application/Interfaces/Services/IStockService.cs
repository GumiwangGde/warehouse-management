using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface IStockService
{
    Task <IReadOnlyList<StockDTO>> GetAllStockAsync();
    Task <StockDTO> GetStockByProductAsync(int productId);
    Task <StockDTO> AdjustStockAsync(UpdateStockRequest request);
}