namespace Ecommerce.Shared.DTOs.Request;


public record AttributeValueDto(
    string Value,
    string AttributeId
);

public record CreateAttributeValueDto(
    string Value,
    string AttributeId
);

public record UpdateAttributeValueDto(
    string Value,
    string AttributeId
);
