using WarehouseManagement.Domain.Common;

namespace WarehouseManagement.Domain.Entities;

public class Stock : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public decimal Quantity { get; set; }
    public decimal MinimumQuantity { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<History> Histories { get; set; } = [];
}