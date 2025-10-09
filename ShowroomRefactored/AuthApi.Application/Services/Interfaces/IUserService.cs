namespace AuthApi.Application.Services.Interfaces;

public interface IUserService
{
    public Task<string> GetIdByEmailAsync(string email);
    
}