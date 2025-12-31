using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Services;

namespace WarehouseManagement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class HistoriesController(IHistoriesService historiesService) : ControllerBase
{
    private readonly IHistoriesService _historyService = historiesService;

    [HttpGet]
    public async Task<ActionResult<HistoryDTO>> GetAllHistory() => 
        Ok(await _historyService.GetAllHistoriesAsync());

    [HttpGet("stock/{stockId}")]
    public async Task<ActionResult<HistoryDTO>> GetByStock(int id) => 
        Ok(await _historyService.GetByStockIdAsync(id));

    [HttpGet("filter")] 
    public async Task<ActionResult<HistoryDTO>> GetHistoryByRange(DateTime startTime, DateTime endTime) => 
        Ok(await _historyService.GetByDateRangeAsync(startTime, endTime));
}