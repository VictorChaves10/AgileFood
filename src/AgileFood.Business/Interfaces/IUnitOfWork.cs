namespace AgileFood.Business.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
    Task ExecuteInTransactionAsync(Func<Task> operation);

    IProductRepository ProductRepository { get; }
    IProductCategoryRepository ProductCategoryRepository { get; }
    IStockItemRepository StockItemRepository { get; }
    IUserRepository UserRepository { get; }
    IConsumptionRepository ConsumptionRepository { get; }
    ICatalogItemRepository CatalogItemRepository { get; }
}
