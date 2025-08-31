namespace Ecommerce.Shared.DTOs.Request;

public record AttributeDto(
    string Id,
    string AttributeName
);

public record CreateAttributeDto(
    string AttributeName
);

public record UpdateAttributeDto(
    string Id,
    string AttributeName
);
