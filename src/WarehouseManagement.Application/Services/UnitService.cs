using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services;

public class UnitService(IGenericRepository<Unit> unitRepo) : IUnitServices
{
    private readonly IGenericRepository<Unit> _unitRepo = unitRepo;

    public async Task<IReadOnlyList<UnitDTO>> GetAllUnitsAsync()
    {
        var units = await _unitRepo.ListAllAsync();

        return [.. units.Select(u => new UnitDTO
        {
           Id = u.Id,
           Name = u.Name,
           Abbreviation = u.Abbreviation
        })];
    }

    public async Task<UnitDTO> CreateUnitAsync(CreateUnitRequest request)
    {
        var unit = new Unit
        {
            Name = request.Name,
            Abbreviation = request.Abbreviation,
        };

        await _unitRepo.AddAsync(unit);
        await _unitRepo.SaveChangesAsync();

        return new UnitDTO
        {
            Id = unit.Id,
            Name = unit.Name,
            Abbreviation = unit.Abbreviation
        };
    }
}