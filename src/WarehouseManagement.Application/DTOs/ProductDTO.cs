namespace WarehouseManagement.Application.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int BaseUnitId { get; set; }
    public bool IsActive { get; set; }
}

public class ProductDetailDTO
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int BaseUnitId { get; set; }
    public string BaseUnitName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class CreateProductRequest
{
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int BaseUnitId { get; set; }
}