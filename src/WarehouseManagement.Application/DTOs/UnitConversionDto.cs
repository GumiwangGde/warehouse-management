namespace WarehouseManagement.Application.DTOs;

public class UnitConversionDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int FromUnitId { get; set; }
    public string FromUnitName { get; set; } = string.Empty;
    public int ToUnitId { get; set; }
    public string ToUnitName { get; set; } = string.Empty;
    public decimal Factor { get; set; }
}

public class CreateUnitConversionRequest
{
    public int ProductId { get; set; }
    public int FromUnitId { get; set; }
    public int ToUnitId { get; set; }
    public decimal Factor { get; set; }
}