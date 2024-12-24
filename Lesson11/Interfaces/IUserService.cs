using Lesson11.Data.DTO.User;
using Lesson11.Data.Model;

namespace Lesson11.Interfaces;

public interface IUserService
{
    public User LoginUser(Login_DTO loginDto);
    public void RegisterUser(Register_DTO registerDto);
    
    public void ForgotPassword(Forgot_DTO forgotDto);
}