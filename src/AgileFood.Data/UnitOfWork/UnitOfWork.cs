using AgileFood.Business.Interfaces;
using AgileFood.Data.Context;
using AgileFood.Data.Repository;

namespace AgileFood.Data.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private bool _disposed;

    private IProductRepository _productRepository;
    private IProductCategoryRepository _productCategoryRepository;
    private IStockItemRepository _stockItemRepository;
    private IUserRepository _userRepository;
    private IConsumptionRepository _consumptionRepository;
    private ICatalogItemRepository _catalogItemRepository;

    public IProductRepository ProductRepository
    {
        get { return _productRepository ??= new ProductRepository(_context); }
    }

    public IProductCategoryRepository ProductCategoryRepository
    {
        get { return _productCategoryRepository ??= new ProductCategoryRepository(_context); }
    }

    public IStockItemRepository StockItemRepository
    {
        get { return _stockItemRepository ??= new StockItemRepository(_context); }
    }

    public IUserRepository UserRepository
    {
        get { return _userRepository ??= new UserRepository(_context); }
    }

    public IConsumptionRepository ConsumptionRepository
    {
        get { return _consumptionRepository ??= new ConsumptionRepository(_context); }
    }

    public ICatalogItemRepository CatalogItemRepository
    {
        get { return _catalogItemRepository ??= new CatalogItemRepository(_context); }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _context.Dispose();
            _disposed = true;
        }

        GC.SuppressFinalize(this);
    }
}