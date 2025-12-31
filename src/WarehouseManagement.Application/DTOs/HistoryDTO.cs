namespace WarehouseManagement.Application.DTOs;

public class HistoryDTO
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string StaffName { get; set; } = string.Empty;
    public string TransactionTypeName { get; set; } = string.Empty;
    public decimal InputAmount { get; set; }
    public string InputUnitName { get; set; } = string.Empty;
    public decimal BaseAmount { get; set; }
    public string BaseUnitName { get; set; } = string.Empty;
    public string TransactionType { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
}