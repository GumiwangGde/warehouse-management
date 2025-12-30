using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface ISupplierService
{
    Task<IReadOnlyList<SupplierDTO>> GetAllSupplierAsync();
    Task<SupplierDTO> GetSupplierByIdAsync(int id);
    Task<SupplierDTO> CreateSupplierAsync(CreateSupplierRequest request);
    Task DeleteSupplierAsync(int id);
}