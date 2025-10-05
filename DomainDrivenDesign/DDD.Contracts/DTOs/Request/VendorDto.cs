namespace DDD.Contracts.DTOs.Request;

public record VendorDto(
    string Id,
    string VendorName
);

public record CreateVendorDto(
    string VendorName
);

public record UpdateVendorDto(
    string Id,
    string VendorName
);
