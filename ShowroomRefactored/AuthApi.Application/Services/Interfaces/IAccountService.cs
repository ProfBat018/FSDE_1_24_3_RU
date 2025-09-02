using System.Security.Claims;
using AuthApi.Contracts.DTOs.Request;
using AuthApi.Contracts.DTOs.Response;
using Microsoft.AspNetCore.Http;

namespace AuthApi.Application.Services.Interfaces;

public interface IAccountService
{
    public Task<Result> RegisterAsync(RegisterRequestDTO requestDto);
    public Task ConfirmEmailAsync(ClaimsPrincipal user, string token, HttpContext context);
    public Task<Result> VerifyEmailAsync(string id);
}