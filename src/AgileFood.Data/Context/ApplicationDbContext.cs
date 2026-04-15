using AgileFood.Business.Models.Catalogs;
using AgileFood.Business.Models.Consumptions;
using AgileFood.Business.Models.Products;
using AgileFood.Business.Models.Stock;
using AgileFood.Business.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace AgileFood.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Consumption> Consumptions { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
