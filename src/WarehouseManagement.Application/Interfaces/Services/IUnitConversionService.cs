using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface IUnitConversionService
{
    Task<IReadOnlyList<UnitConversionDTO>> GetConversionsByProductAsync(int productId);
    Task<UnitConversionDTO> CreateConversionAsync(CreateUnitConversionRequest request);
}