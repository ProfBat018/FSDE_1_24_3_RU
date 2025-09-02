using System.Security.Claims;
using AuthApi.Data.Models;

namespace AuthApi.Application.Services.Interfaces;

public interface ITokenService
{
    public Task<string> CreateTokenAsync(User user, List<string> roles);
    public Task<string> CreateEmailTokenAsync(ClaimsPrincipal user);
    public Task<bool> ValidateEmailTokenAsync(string token);
    public Task<string> GetEmailFromToken(string token);
}