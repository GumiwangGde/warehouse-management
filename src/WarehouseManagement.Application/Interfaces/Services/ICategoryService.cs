using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDTO>> GetAllCategoryAsync();
    Task<CategoryDTO> CreateCategoryAsync(CreateCategoryRequest request);

    Task DeleteCategoryAsync(int id);
}