namespace ContactService.Contracts.DTOs;

public record UpsertContactDto(
    string? PhoneNumber,
    string? Email
);