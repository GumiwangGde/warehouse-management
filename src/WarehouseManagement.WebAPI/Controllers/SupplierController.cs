using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Services;

namespace WarehouseManagement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]

public class SupplierController(ISupplierService supplierService) : ControllerBase
{
    private readonly ISupplierService _supplierService = supplierService;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SupplierDTO>>> GetSuppliers() => 
        Ok(await _supplierService.GetAllSupplierAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDTO>> GetSupplier(int id) =>
        Ok(await _supplierService.GetSupplierByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<SupplierDTO>> CreateSupplier(CreateSupplierRequest request) => 
        Ok(await _supplierService.CreateSupplierAsync(request));

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSupplier(int id)
    {
        await _supplierService.DeleteSupplierAsync(id);
        return Ok(new { Message = "Supplier deleted" });
    }
}