using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Services;

namespace WarehouseManagement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    /// <summary>
    ///  Get all list of product
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetProducts()
    {
        var result = await _productService.GetAllProductAsync();
        return Ok(result);
    }

    /// <summary>
    /// Get selected product by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    /// <summary>
    /// Adding new product to the warehouse
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductRequest request)
    {
        var result = await _productService.CreateProductAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Delete product from the warehouse
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return Ok(new { Message = $"Product with ID {id} deleted" });
    }

}