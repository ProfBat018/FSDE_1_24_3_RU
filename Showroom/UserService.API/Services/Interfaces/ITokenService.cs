using UserService.Data.Data.Models;

namespace UserService.API.Services.Interfaces;

public interface ITokenService
{
    public Task<string> CreateTokenAsync(User user, List<string> roles);
}