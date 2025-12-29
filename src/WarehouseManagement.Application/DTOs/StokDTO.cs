namespace WarehouseManagement.Application.DTOs;

public class StockDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal MinimumQuantity { get; set; }
    public string BaseUnitName { get; set; } = string.Empty;
    public bool IsLowStock => Quantity <= MinimumQuantity;
}

public class UpdateStockRequest
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public decimal Amount { get; set; }
    public int UnitId { get; set; }
}