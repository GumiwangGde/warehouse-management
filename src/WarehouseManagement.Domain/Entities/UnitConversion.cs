using WarehouseManagement.Domain.Common;

namespace WarehouseManagement.Domain.Entities;

public class UnitConversion : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int FromUnitId { get; set; }
    public Unit? FromUnit { get; set; }
    public int ToUnitId { get; set; }
    public Unit? ToUnit { get; set; }
    public decimal Factor { get; set; }
}