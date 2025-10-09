namespace DDD.Contracts.DTOs.Request;

public record OrderDto(
    string Id,
    string UserId,
    string HubId,
    double TotalPrice
);

public record CreateOrderDto(
    string UserId,
    string HubId,
    double TotalPrice
);

public record UpdateOrderDto(
    string Id,
    double TotalPrice
);
