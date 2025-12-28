using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductDTO>> GetAllProductAsync();
    Task<ProductDetailDTO> GetByIdAsync(int id);
    Task<ProductDTO> CreateProductAsync(CreateProductRequest request);
    Task DeleteProductAsync(int id);
}