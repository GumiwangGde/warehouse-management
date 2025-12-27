using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UnitsController(IUnitServices unitServices) : ControllerBase
{
    private readonly IUnitServices _unitServices = unitServices;

    /// <summary>
    ///  Get all list of units
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UnitDTO>>> GetUnits()
    {
        var units = await _unitServices.GetAllUnitsAsync();
        return Ok(units);
    }

    /// <summary>
    ///  Get new unit to the warehouse
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UnitDTO>> CreateUnit(CreateUnitRequest request)
    {
        var result = await _unitServices.CreateUnitAsync(request);
        return Ok(result);
    }
} 