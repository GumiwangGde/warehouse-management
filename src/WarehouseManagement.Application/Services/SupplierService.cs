using System.Linq.Expressions;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.Application.Services;

public class SupplierService (IGenericRepository<Supplier> supplierRepo) : ISupplierService
{
    private readonly IGenericRepository<Supplier> _supplierRepo = supplierRepo;

    public async Task<IReadOnlyList<SupplierDTO>> GetAllSupplierAsync()
    {
        var supplier = await _supplierRepo.ListAllAsync();

        return [.. supplier.Select(s => new SupplierDTO {
            Id = s.Id,
            Code = s.Code,
            Name = s.Name,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber
        })];
    }

    public async Task<SupplierDTO> GetSupplierByIdAsync(int id)
    {
        var supplier = await _supplierRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("Supplier not found");

        return new SupplierDTO
        {
            Id = supplier.Id,
            Code = supplier.Code,
            Name = supplier.Name,
            Address = supplier.Address,
            PhoneNumber = supplier.PhoneNumber
        };
    }

    public async Task<SupplierDTO> CreateSupplierAsync(CreateSupplierRequest request)
    {
        var all = await _supplierRepo.ListAllAsync();
        if (all.Any(s => s.Code == request.Code))
            throw new Exception("Supplier code already used");

        var supplier = new Supplier
        {
            Code = request.Code,
            Name = request.Name,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };

        await _supplierRepo.AddAsync(supplier);
        await _supplierRepo.SaveChangesAsync();

        return new SupplierDTO
        {
            Id = supplier.Id,
            Code = supplier.Code,
            Name = supplier.Name,
            Address = supplier.Address,
            PhoneNumber = supplier.PhoneNumber
        };
    }

    public async Task DeleteSupplierAsync(int id)
    {
        var supplier = await _supplierRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("Supplier not found");

        _supplierRepo.Delete(supplier);
        await _supplierRepo.SaveChangesAsync();
    }
}