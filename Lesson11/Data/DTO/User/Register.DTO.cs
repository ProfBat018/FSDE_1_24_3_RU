namespace Lesson11.Data.DTO.User;

public record Register_DTO(
    string username,
    string password,
    string confirmPassword,
    string email,
    DateTime dateOfBirth);
    
    
    