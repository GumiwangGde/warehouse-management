using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Services;

namespace WarehouseManagement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class StocksController(IStockService stockService) : ControllerBase
{
    private readonly IStockService _stockService = stockService;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StockDTO>>> GetAll()
    {
        var result = await _stockService.GetAllStockAsync();
        return Ok(result);
    }

    [HttpGet("products/{productId}")]
    public async Task<ActionResult<StockDTO>> GetByProductId(int productId)
    {
        var result = await _stockService.GetStockByProductAsync(productId);
        return Ok(result);
    }

    [HttpPost("adjust")]
    public async Task<ActionResult<StockDTO>> AdjustStock(UpdateStockRequest request)
    {
        var result = await _stockService.AdjustStockAsync(request);
        return Ok(result);
    }
}