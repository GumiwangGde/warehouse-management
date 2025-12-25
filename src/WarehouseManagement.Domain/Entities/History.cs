using WarehouseManagement.Domain.Common;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Domain.Entities;

public class History : BaseEntity
{
    public int StockId { get; set; }
    public Stock? Stock { get; set; }
    public int StaffId { get; set; }
    public Staff? Staff { get; set; }
    public int InputUnitId { get; set; }
    public Unit? InputUnit { get; set; }
    public decimal InputAmount { get; set; }
    public decimal BaseAmount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}