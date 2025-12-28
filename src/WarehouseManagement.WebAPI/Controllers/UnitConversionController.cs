using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Services;

namespace WarehouseManagement.WebAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class UnitConversionController(IUnitConversionService conversionService) : ControllerBase
{
    private readonly IUnitConversionService _conversionService = conversionService;

    /// <summary>
    /// Adding new ConversionUnit to the warehouse
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UnitConversionDTO>> Create(CreateUnitConversionRequest request)
    {
        var result = await _conversionService.CreateConversionAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Get selected conversion by productId
    /// </summary>
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IReadOnlyList<UnitConversionDTO>>> GetConversionsByProductAsync(int productId)
    {
        var result = await _conversionService.GetConversionsByProductAsync(productId);
        return Ok(result);
    }
}