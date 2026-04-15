namespace AgileFood.Application.Dtos.Catalogs;

public record UpdateCatalogItemDto(
    long Id,
    bool IsAvailable
);