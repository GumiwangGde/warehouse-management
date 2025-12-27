using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<UnitConversion> UnitConversions => Set<UnitConversion>();
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<History> Histories => Set<History>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Staff> Staffs => Set<Staff>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UnitConversion>(entity =>
        {
            entity.HasOne(uc => uc.FromUnit)
                .WithMany(u => u.FromUnitConversions)
                .HasForeignKey(uc => uc.FromUnitId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(uc => uc.ToUnit)
                .WithMany(u => u.ToUnitConversions)
                .HasForeignKey(uc => uc.ToUnitId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(uc => uc.Product)
                .WithMany(p => p.UnitConversions)
                .HasForeignKey(uc => uc.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}