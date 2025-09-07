namespace UserService.Contracts.DTOs;

public record UserContactsDto(
    string UserId,
    string PhoneNumber,
    bool PhoneVerified,
    string Email,
    bool EmailVerified
);