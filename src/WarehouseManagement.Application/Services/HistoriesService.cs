using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services;

public class HistoriesService(
    IGenericRepository<History> historyRepo,
    IGenericRepository<Stock> stockRepo,
    IGenericRepository<Product> productRepo,
    IGenericRepository<Unit> unitRepo,
    IGenericRepository<Supplier> supplierRepo,
    IGenericRepository<Staff> staffRepo
) : IHistoriesService
{
    private readonly IGenericRepository<History> _historyRepo = historyRepo;
    private readonly IGenericRepository<Stock> _stockRepo = stockRepo;
    private readonly IGenericRepository<Product> _productRepo = productRepo;
    private readonly IGenericRepository<Unit> _unitRepo = unitRepo;
    private readonly IGenericRepository<Supplier> _supplierRepo = supplierRepo;
    private readonly IGenericRepository<Staff> _staffRepo = staffRepo;

    public async Task<IReadOnlyList<HistoryDTO>> GetAllHistoriesAsync()
    {
        var histories = await _historyRepo.ListAllAsync();
        var stocks = await _stockRepo.ListAllAsync();
        var products = await _productRepo.ListAllAsync();
        var units = await _unitRepo.ListAllAsync();
        var suppliers = await _supplierRepo.ListAllAsync();
        var staffs = await _staffRepo.ListAllAsync();

        return MapToDTOList(histories, stocks, products, units, suppliers, staffs);
    }

    public async Task<IReadOnlyList<HistoryDTO>> GetByStockIdAsync(int stockId)
    {
        var all = await _historyRepo.ListAllAsync();
        var filtered = all.Where(h => h.StockId == stockId).ToList();

        var stocks = await _stockRepo.ListAllAsync();
        var products = await _productRepo.ListAllAsync();
        var units = await _unitRepo.ListAllAsync();
        var suppliers = await _supplierRepo.ListAllAsync();
        var staffs = await _staffRepo.ListAllAsync();

        return MapToDTOList(filtered, stocks, products, units, suppliers, staffs);
    }

    public async Task<IReadOnlyList<HistoryDTO>> GetByDateRangeAsync(DateTime startTime, DateTime endTime)
    {
        var all = await _historyRepo.ListAllAsync();
        var filtered = all.Where(h => h.TransactionDate >= startTime && h.TransactionDate <= endTime);

        var stocks = await _stockRepo.ListAllAsync();
        var products = await _productRepo.ListAllAsync();
        var units = await _unitRepo.ListAllAsync();
        var suppliers = await _supplierRepo.ListAllAsync();
        var staffs = await _staffRepo.ListAllAsync();

        return MapToDTOList(filtered, stocks, products, units, suppliers, staffs);
    }

    private List<HistoryDTO> MapToDTOList(
        IEnumerable<History> histories,
        IReadOnlyList<Stock> stocks,
        IReadOnlyList<Product> products,
        IReadOnlyList<Unit> units,
        IReadOnlyList<Supplier> suppliers,
        IReadOnlyList<Staff> staffs
    )
    {
        return [.. histories.OrderByDescending(h => h.TransactionDate).Select(h =>
        {
            var stock = stocks.FirstOrDefault(s => s.Id == h.StockId);
            var product = products.FirstOrDefault(p => p.Id == (stock?.ProductId ?? 0));
            var supplier = suppliers.FirstOrDefault(sup => sup.Id == (stock?.SupplierId ?? 0));
            var staff = staffs.FirstOrDefault(st => st.Id == h.StaffId);
            var inputUnit = units.FirstOrDefault(u => u.Id == h.InputUnitId);
            var baseUnit = units.FirstOrDefault(u => u.Id == (product?.BaseUnitId ?? 0));

            return new HistoryDTO
            {
                Id = h.Id,
                StockId = h.StockId,
                ProductName = product?.Name ?? "Unknown Product",
                SupplierName = supplier?.Name ?? "Unknown Supplier",
                StaffName = staff?.Name ?? "Unknown Staff",
                InputUnitName = h.InputUnit?.Name ?? "N/A",
                InputAmount = h.InputAmount,
                BaseAmount = h.BaseAmount,
                BaseUnitName = baseUnit?.Name ?? "N/A",
                TransactionType = h.Type.ToString(),
                TransactionDate = h.TransactionDate
            };
        })];
    }
}