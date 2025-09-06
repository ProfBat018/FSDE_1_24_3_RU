namespace DDD.Contracts.DTOs.Request;

public record OrderProductDto(
    string OrderId,
    string ProductId,
    int ProductCount
);

public record CreateOrderProductDto(
    string OrderId,
    string ProductId,
    int ProductCount
);

public record UpdateOrderProductDto(
    string OrderId,
    string ProductId,
    int ProductCount
);
