namespace AgileFood.Business.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
   
        IProductRepository ProductRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IStockItemRepository StockItemRepository { get; }
    }
}
