namespace DDD.Contracts.DTOs.Request;

public record WarehouseDto(
    string Id,
    string Address
);

public record CreateWarehouseDto(
    string Address
);

public record UpdateWarehouseDto(
    string Id,
    string Address
);
