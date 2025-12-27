using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoriesController(IGenericRepository<Category> categoryRepo) : ControllerBase
{
    private readonly IGenericRepository<Category> _categoryRepo = categoryRepo;

    /// <summary>
    ///  Get all list of category
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
    {
        var categories = await _categoryRepo.ListAllAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Adding new category to the warehouse
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        await _categoryRepo.AddAsync(category);
        await _categoryRepo.SaveChangesAsync();

        return Ok(category);
    }

    /// <summary>
    /// Delete category from the warehouse
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound($"Category with ID {id} not found");
        }

        _categoryRepo.Delete(category);
        await _categoryRepo.SaveChangesAsync();

        return NoContent();
    }
}