using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController(
    IGenericRepository<Product> productRepo,
    IGenericRepository<Category> categoryRepo
    ) : ControllerBase
{
    private readonly IGenericRepository<Product> _productRepo = productRepo;
    private readonly IGenericRepository<Category> _categoryRepo = categoryRepo;

    /// <summary>
    ///  Get all list of product
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct()
    {
        return Ok(await _productRepo.ListAllAsync());
    }

    /// <summary>
    /// Get selected product by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound($"Product with ID {id} not found");
        }

        return Ok(product);
    }

    /// <summary>
    /// Adding new product to the warehouse
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        var category = await _categoryRepo.GetByIdAsync(product.CategoryId);

        if (category == null)
        {
            return BadRequest($"Category with ID {product.Id} not found");
        }

        await _productRepo.AddAsync(product);
        await _productRepo.SaveChangesAsync();

        return Ok(product);
    }

    /// <summary>
    /// Delete products from the warehouse
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound($"Product with ID {id} not found");
        }

        _productRepo.Delete(product);
        await _productRepo.SaveChangesAsync();

        return NoContent();
    }
}