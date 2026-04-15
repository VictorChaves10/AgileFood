using AgileFood.Application.Dtos.Catalogs;
using AgileFood.Business.Models.Catalogs;

namespace AgileFood.Application.Mappings.Catalogs;

public static class CatalogItemExtensions
{
    public static CatalogItemResultDto MapToCatalogItemDto(this CatalogItem catalogItem)
    {
        ArgumentNullException.ThrowIfNull(catalogItem);

        return new CatalogItemResultDto(
            catalogItem.Id,
            catalogItem.ProductId,
            catalogItem.Product?.Name ?? string.Empty,
            catalogItem.Product?.Description,
            catalogItem.Product?.Brand,
            catalogItem.Product?.Flavor,
            catalogItem.Product?.Price ?? 0,
            catalogItem.Product?.Image,
            catalogItem.Product?.ProductCategory?.Name,
            catalogItem.IsAvailable
        );
    }
}