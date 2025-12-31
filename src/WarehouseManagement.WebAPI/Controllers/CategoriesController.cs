using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Services;

namespace WarehouseManagement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    /// <summary>
    ///  Get all list of category
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDTO>>> GetCategories()
    {
        var result = await _categoryService.GetAllCategoryAsync();
        return Ok(result);
    }

    /// <summary>
    /// Adding new category to the warehouse
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryRequest request)
    {
        var result = await _categoryService.CreateCategoryAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Delete category from the warehouse
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return Ok(new { Message = $"Category with {id} deleted" });
    }
}