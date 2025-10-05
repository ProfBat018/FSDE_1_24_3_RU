using System;
using System.Collections.Generic;
namespace DDD.Contracts.DTOs.Request;


public record ProductDto(
    string Id,
    string ProductName,
    string? Description,
    string VendorId,
    IReadOnlyList<string> CategoryIds,
    IReadOnlyList<ProductImageDto> Images
);

public record ProductListItemDto(
    string Id,
    string ProductName,
    string VendorId
);

public record CreateProductDto(
    string ProductName,
    string? Description,
    string VendorId,
    IReadOnlyList<string> CategoryIds,
    IReadOnlyList<CreateProductImageDto> Images
);

public record UpdateProductDto(
    string Id,
    string ProductName,
    string? Description,
    string VendorId,
    IReadOnlyList<string> CategoryIds
);
