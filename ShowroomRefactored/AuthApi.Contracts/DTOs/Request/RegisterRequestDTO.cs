namespace AuthApi.Contracts.DTOs.Request;


public record RegisterRequestDTO(string Email, string Name, string Surname, string Password, string ConfirmPassword);
