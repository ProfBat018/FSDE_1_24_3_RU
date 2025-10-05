namespace ContactService.Contracts.DTOs;

public record ContactDto(
    string UserId,
    string? PhoneNumber,
    bool PhoneVerified,
    string? Email,
    bool EmailVerified
);