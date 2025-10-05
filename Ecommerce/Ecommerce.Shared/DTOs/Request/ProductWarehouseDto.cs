namespace Ecommerce.Shared.DTOs.Request;

public record ProductWarehouseDto(
    string Id,
    string ProductId,
    string WarehouseId,
    int Count,
    double Price
);

public record CreateProductWarehouseDto(
    string ProductId,
    string WarehouseId,
    int Count,
    double Price
);

public record UpdateProductWarehouseDto(
    string Id,
    int Count,
    double Price
);
