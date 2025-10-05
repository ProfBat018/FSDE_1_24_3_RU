namespace UserService.Contracts.DTOs;

public record UserDto(
    string Id,
    string Name,
    string Surname,
    string Email,
    Guid? ImageId
);