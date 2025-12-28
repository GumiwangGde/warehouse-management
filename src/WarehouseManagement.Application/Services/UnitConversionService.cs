using Microsoft.VisualBasic;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services;

public class UnitConversionService(
    IGenericRepository<UnitConversion> conversionRepo,
    IGenericRepository<Product> productRepo,
    IGenericRepository<Unit> unitRepo
    ) : IUnitConversionService
{
    private readonly IGenericRepository<UnitConversion> _conversionRepo = conversionRepo;
    private readonly IGenericRepository<Product> _productRepo = productRepo;
    private readonly IGenericRepository<Unit> _unitRepo = unitRepo;

    public async Task<IReadOnlyList<UnitConversionDTO>> GetConversionsByProductAsync(int productId)
    {
        var allConversions = await _conversionRepo.ListAllAsync();
        var filteredConversions = allConversions.Where(c => c.ProductId == productId).ToList();

        if (!filteredConversions.Any()) return [];

        var product = await _productRepo.GetByIdAsync(productId);
        var allUnits = await _unitRepo.ListAllAsync();

        return [.. filteredConversions.Select(c => new UnitConversionDTO
        {
            Id = c.Id,
            ProductId = c.ProductId,
            ProductName = product?.Name ?? "Unknown",
            FromUnitId = c.FromUnitId,
            FromUnitName = allUnits.FirstOrDefault(u => u.Id == c.FromUnitId)?.Name ?? "Unknown",
            ToUnitId = c.ToUnitId,
            ToUnitName = allUnits.FirstOrDefault(u => u.Id == c.ToUnitId)?.Name ?? "Unknown",
            Factor = c.Factor
        })];
    }

    public async Task<UnitConversionDTO> CreateConversionAsync(CreateUnitConversionRequest request)
    {
        var conversion = new UnitConversion
        {
            ProductId = request.ProductId,
            FromUnitId = request.FromUnitId,
            ToUnitId = request.ToUnitId,
            Factor = request.Factor,
        };

        await _conversionRepo.AddAsync(conversion);
        await _conversionRepo.SaveChangesAsync();

        var product = await _productRepo.GetByIdAsync(conversion.ProductId);
        var fromUnit = await _unitRepo.GetByIdAsync(conversion.FromUnitId);
        var toUnit = await _unitRepo.GetByIdAsync(conversion.ToUnitId);

        return new UnitConversionDTO
        {
            Id = conversion.Id,
            ProductId = conversion.ProductId,
            ProductName = product?.Name ?? "Unknown",
            FromUnitId = conversion.FromUnitId,
            FromUnitName = fromUnit?.Name ?? "Unknown",
            ToUnitId = conversion.ToUnitId,
            ToUnitName = toUnit?.Name ?? "Unknown",
            Factor = conversion.Factor
        };
    }
}