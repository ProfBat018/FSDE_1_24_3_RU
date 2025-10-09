namespace Ecommerce.Shared.DTOs.Request;

public record CategoryAttributesDto(
    string CategoryId,
    string AttributeId
);

public record CreateCategoryAttributesDto(
    string CategoryId,
    string AttributeId
);
