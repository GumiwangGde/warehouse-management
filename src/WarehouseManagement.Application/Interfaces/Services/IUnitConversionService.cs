using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface IUnitConversionService
{
    Task<IReadOnlyList<UnitConversionDTO>> GetConvesionsByProductAsync(int productId);
    Task<UnitConversionDTO> CreateUnitAsync(CreateUnitConversionRequest request);
}