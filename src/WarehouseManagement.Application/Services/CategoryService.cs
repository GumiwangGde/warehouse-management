using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.Application.Services;

public class CategoryService (IGenericRepository<Category> categoryRepo) : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepo = categoryRepo;

    public async Task<IReadOnlyList<CategoryDTO>> GetAllCategoryAsync()
    {
        var categories = await _categoryRepo.ListAllAsync();
        return [.. categories
            .Select(c => new CategoryDTO 
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            })];
    }

    public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description,
        };

        await _categoryRepo.AddAsync(category);
        await _categoryRepo.SaveChangesAsync();

        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);
        if (category == null) throw new NotFoundException("Category not found");

        _categoryRepo.Delete(category);
        await _categoryRepo.SaveChangesAsync();
    }
}