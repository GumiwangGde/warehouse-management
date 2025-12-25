using System.Collections;
using WarehouseManagement.Domain.Common;

namespace WarehouseManagement.Domain.Entities;

public class Product : BaseEntity
{
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int BaseUnitId { get; set; }
    public Unit? BaseUnit { get; set; } 
    public bool IsActive { get; set; } = true;

    public ICollection<UnitConversion> UnitConversions { get; set; } = [];
}