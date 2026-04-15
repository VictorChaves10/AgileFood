namespace AgileFood.Application.Dtos.Catalogs;

public record CatalogItemResultDto(
    long Id,
    long ProductId,
    string ProductName,
    string? ProductDescription,
    string? ProductBrand,
    string? ProductFlavor,
    decimal ProductPrice,
    string? ProductImage,
    string? CategoryName,
    bool IsAvailable
);