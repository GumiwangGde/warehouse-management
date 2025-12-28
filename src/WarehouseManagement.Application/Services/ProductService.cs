using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.Application.Services;

public class ProductService(
    IGenericRepository<Product> productRepo,
    IGenericRepository<Unit> unitRepo,
    IGenericRepository<Category> categoryRepo) : IProductService
{
    private readonly IGenericRepository<Product> _productRepo = productRepo;
    private readonly IGenericRepository<Category> _categoryRepo = categoryRepo;
    private readonly IGenericRepository<Unit> _unitRepo = unitRepo;

    public async Task<IReadOnlyList<ProductDTO>> GetAllProductAsync()
    {
        var products = await _productRepo.ListAllAsync();
        return [ ..products
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                CategoryId = p.CategoryId,
                BaseUnitId = p.BaseUnitId,
                IsActive = p.IsActive,
            })];
    }

    public async Task<ProductDetailDTO> GetByIdAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null) throw new NotFoundException("Product not found");

        var category = await _categoryRepo.GetByIdAsync(product.CategoryId);
        var unit = await _unitRepo.GetByIdAsync(product.BaseUnitId);

        return new ProductDetailDTO
        {
            Id = product.Id,
            SKU = product.SKU,
            Name = product.Name,
            CategoryId = product.CategoryId,
            CategoryName = category?.Name ?? "N/A",
            BaseUnitId = product.BaseUnitId,
            BaseUnitName = unit?.Name ?? "N/A",
            IsActive = product.IsActive,
        };
    }

    public async Task<ProductDTO> CreateProductAsync(CreateProductRequest request)
    {
        var category = await _categoryRepo.GetByIdAsync(request.CategoryId);
        if (category == null) throw new NotFoundException("Category not found");

        var product = new Product
        {
            SKU = request.SKU,
            Name = request.Name,
            CategoryId = request.CategoryId,
            BaseUnitId = request.BaseUnitId,
            IsActive = true,
        };

        await _productRepo.AddAsync(product);
        await _productRepo.SaveChangesAsync();

        return new ProductDTO
        {
            Id = product.Id,
            SKU = product.SKU,
            Name = product.Name,
            CategoryId = product.CategoryId,
            BaseUnitId = product.BaseUnitId,
            IsActive = product.IsActive,
        };
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null) throw new NotFoundException("Product not found");

        product.IsActive = false;
        _productRepo.Update(product);
        await _productRepo.SaveChangesAsync();
    }
}