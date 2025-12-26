using WarehouseManagement.Domain.Common;

namespace WarehouseManagement.Domain.Entities;

public class Unit : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;

    public ICollection<UnitConversion> FromUnitConversions { get; set; } = [];
    public ICollection<UnitConversion> ToUnitConversions { get; set; } = [];
    public ICollection<History> Histories { get; set; } = [];
}