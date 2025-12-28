using Microsoft.VisualBasic;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services;

public class UnitConversionService(IGenericRepository<UnitConversion> conversionRepo) : IUnitConversionService
{
    private readonly IGenericRepository<UnitConversion> _conversionRepo = conversionRepo;

    public async Task<IReadOnlyList<UnitConversionDTO>> GetConversionsByProductAsync(int productId)
    {
        var conversions = await _conversionRepo.ListAllAsync();

        return [.. conversions
            .Where(c => c.ProductId == productId)
            .Select(c => new UnitConversionDTO
            {
                Id = c.Id,
                ProductId = c.ProductId,
                FromUnitId = c.FromUnitId,
                ToUnitId = c.ToUnitId,
                Factor = c.Factor,
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

        return new UnitConversionDTO
        {
            Id = conversion.Id,
            ProductId = conversion.ProductId,
            FromUnitId = conversion.FromUnitId,
            ToUnitId = conversion.ToUnitId,
            Factor = conversion.Factor
        };
    }
}