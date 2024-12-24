using System.Text.RegularExpressions;
using Lesson11.Data.DTO.User;

namespace Lesson11.Implementations;

public static class ValidateService
{
    private static Regex loginRegex = new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z]([A-Za-z\d.-_]){0,19}$");
    private static Regex passwordRegex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%&_])[A-Za-z\d!@#$%&_]{8,16}$");
    private static Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+(\.[a-zA-Z]{2,}){1,}$");
    
    public static bool ValidateLogin(Login_DTO loginDto)
    {
        return loginRegex.IsMatch(loginDto.username) && passwordRegex.IsMatch(loginDto.password);
    }
    
    public static bool ValidateRegister(Register_DTO registerDto)
    {
        return loginRegex.IsMatch(registerDto.username) && passwordRegex.IsMatch(registerDto.password) && emailRegex.IsMatch(registerDto.email);
    }
    
    public static bool ValidateForgot(Forgot_DTO forgotDto)
    {
        return emailRegex.IsMatch(forgotDto.email);
    }
    
}