namespace DDD.Contracts.DTOs.Request;


public record CategoryDto(
    string CategoryName,
    string? ParentCategoryName
);

public record CreateCategoryDto(
    string CategoryName,
    string? ParentCategoryName
);

public record UpdateCategoryDto(
    string CategoryName,
    string? ParentCategoryName
);
