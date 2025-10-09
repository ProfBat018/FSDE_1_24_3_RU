namespace UserService.API.DTOs.Requests;

public record RegisterRequestDTO(string Email, string Name, string Surname, string Password, string ConfirmPassword);
