namespace UserService.Contracts.DTOs;

public record CreateUserDto(
    string Id,
    string Name,
    string Surname,
    string Email,
    Guid ImageId
);