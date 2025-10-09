namespace DDD.Contracts.DTOs.Request;

public record ProductCategoryDto(
    string ProductCategoryId,
    string ProductId,
    string CategoryId
);

public record CreateProductCategoryDto(
    string ProductId,
    string CategoryId
);
