namespace Ecommerce.Shared.DTOs.Request;

public record ProductImageDto(
    string ImageName,
    string ProductId,
    bool IsMain,
    string ImagePath
);

public record CreateProductImageDto(
    string ImageName,
    bool IsMain,
    string ImagePath
);

public record UpdateProductImageDto(
    string ImageName,
    bool IsMain,
    string ImagePath
);
