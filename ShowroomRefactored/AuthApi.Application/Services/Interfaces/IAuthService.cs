using AuthApi.Contracts.DTOs.Request;
using AuthApi.Contracts.DTOs.Response;

namespace AuthApi.Application.Services.Interfaces;

public interface IAuthService
{
    public Task<TypedResult<object>> LoginAsync(LoginRequestDTO request);
}