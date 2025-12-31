using System.Transactions;
using Microsoft.VisualBasic;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.Application.Services;

public class StockService(
    IGenericRepository<Stock> stockRepo,
    IGenericRepository<Product> productRepo,
    IGenericRepository<UnitConversion> conversionRepo,
    IGenericRepository<Unit> unitRepo,
    IGenericRepository<Supplier> supplierRepo, 
    IGenericRepository<History> historyRepo) : IStockService
{
    private readonly IGenericRepository<Stock> _stockRepo = stockRepo;
    private readonly IGenericRepository<Product> _productRepo = productRepo;
    private readonly IGenericRepository<UnitConversion> _conversionRepo = conversionRepo;
    private readonly IGenericRepository<Unit> _unitRepo = unitRepo;
    private readonly IGenericRepository<Supplier> _supplierRepo = supplierRepo;
    private readonly IGenericRepository<History> _historyRepo = historyRepo;

    public async Task<StockDTO> AdjustStockAsync(UpdateStockRequest request)
    {
        var product = await _productRepo.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException("Product not found");

        var supplier = await _supplierRepo.GetByIdAsync(request.SupplierId)
            ?? throw new NotFoundException("Supplier not found");

        decimal finalQuantity = request.Amount;

        if (request.UnitId != product.BaseUnitId)
        {
            var conversions = await _conversionRepo.ListAllAsync();
            var conversion = conversions.FirstOrDefault(c =>
                c.ProductId == request.ProductId &&
                c.FromUnitId == request.UnitId && 
                c.ToUnitId == product.BaseUnitId);

            if (conversion == null) throw new Exception($"Conversion rule from Unit {request.UnitId} to Base Unit not found.");
            
            finalQuantity = request.Amount * conversion.Factor;
        }

        var allStocks = await _stockRepo.ListAllAsync();
        var stock = allStocks.FirstOrDefault(s =>  s.ProductId == request.ProductId && s.SupplierId == request.SupplierId);

        if (stock == null)
        {
            stock = new Stock
            {
                ProductId = request.ProductId,
                SupplierId = request.SupplierId,
                Quantity = finalQuantity,
                MinimumQuantity = 10
            };
            await _stockRepo.AddAsync(stock);
        }
        else
        {
            if (request.Type == TransactionType.Outbound)
            {
                stock.Quantity -= finalQuantity;
            }
            else if (request.Type == TransactionType.Inbound)
            {
                stock.Quantity += finalQuantity;
            }
            else if (request.Type == TransactionType.Adjustment)
            {
                stock.Quantity += finalQuantity;
            }
        }

        await _stockRepo.SaveChangesAsync();

        var history = new History
        {
            StockId = stock.Id,
            StaffId = request.StaffId,
            InputUnitId = request.UnitId,
            InputAmount = request.Amount,
            BaseAmount = finalQuantity,
            Type = request.Type,
            TransactionDate = DateTime.UtcNow
        };

        await _historyRepo.AddAsync(history);
        await _historyRepo.SaveChangesAsync();

        return new StockDTO
        {
            Id = stock.Id,
            ProductId = stock.ProductId,
            ProductName = product.Name,
            SupplierName = supplier.Name,
            Quantity = stock.Quantity,
            MinimumQuantity = stock.MinimumQuantity
        };
    }

    public async Task<IReadOnlyList<StockDTO>> GetAllStockAsync()
    {
        var stocks = await _stockRepo.ListAllAsync();
        var products = await _productRepo.ListAllAsync();
        var suppliers = await _supplierRepo.ListAllAsync();
        var units = await _unitRepo.ListAllAsync();

        return [.. stocks.Select(s => 
        {
            var product = products.FirstOrDefault(p => p.Id == s.ProductId);
            var supplier = suppliers.FirstOrDefault(sup => sup.Id == s.SupplierId);
            var unit = units.FirstOrDefault(u => u.Id == (product?.BaseUnitId ?? 0));

            return new StockDTO{
                Id = s.Id,
                ProductId = s.ProductId,
                ProductName = product?.Name ?? "Unknown Product",
                SKU = product?.SKU ?? "N/A",
                SupplierId = s.SupplierId,
                SupplierName = supplier?.Name ?? "Unknown Supplier",
                Quantity = s.Quantity,
                MinimumQuantity = s.MinimumQuantity,
                BaseUnitName = unit?.Name ?? "N/A"
            };
        })];
    }

    public async Task<StockDTO> GetStockByProductAsync(int productId)
    {
        var allStocks = await _stockRepo.ListAllAsync();
        var stock = allStocks.FirstOrDefault(s => s.ProductId == productId);

        if (stock == null) throw new NotFoundException("Stock for product not found");

        var product = await _productRepo.GetByIdAsync(productId);
        var supplier = await _supplierRepo.GetByIdAsync(stock.SupplierId);
        var unit = await _unitRepo.GetByIdAsync(product?.BaseUnitId ?? 0);

        return new StockDTO
        {
            Id = stock.Id,
            ProductId = stock.ProductId,
            ProductName = product?.Name ?? "Unknown Product",
            SKU = product?.SKU ?? "N/A",
            SupplierId = stock.SupplierId,
            SupplierName = supplier?.Name ?? "Unknown Supplier",
            Quantity = stock.Quantity,
            MinimumQuantity = stock.MinimumQuantity,
            BaseUnitName = unit?.Name ?? "N/A"
        };
    }
}