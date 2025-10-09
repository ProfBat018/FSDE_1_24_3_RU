namespace DDD.Contracts.DTOs.Request;

public record HubObjectDto(
    string Id,
    string HubName,
    string ProductWarehouseId
);

public record CreateHubObjectDto(
    string HubName,
    string ProductWarehouseId
);

public record UpdateHubObjectDto(
    string Id,
    string HubName,
    string ProductWarehouseId
);
