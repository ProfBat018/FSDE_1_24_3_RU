using System.Security.Claims;
using UserService.API.DTOs.Requests;
using UserService.API.DTOs.Response;

namespace UserService.API.Services.Interfaces;

public interface IAccountService
{
    public Task<Result> RegisterAsync(RegisterRequestDTO requestDto);
    public Task ConfirmEmailAsync(ClaimsPrincipal user, string token, HttpContext context);
    public Task<Result> VerifyEmailAsync(string id);
}