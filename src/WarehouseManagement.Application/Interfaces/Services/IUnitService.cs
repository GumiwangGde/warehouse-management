using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface IUnitServices
{
    Task<IReadOnlyList<UnitDTO>> GetAllUnitsAsync();
    Task<UnitDTO> CreateUnitAsync(CreateUnitRequest request);
}