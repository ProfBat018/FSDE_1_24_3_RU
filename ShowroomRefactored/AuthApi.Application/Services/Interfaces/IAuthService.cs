using AuthApi.Core.DTOs.Request;
using AuthApi.Core.DTOs.Response;

namespace AuthApi.Application.Services.Interfaces;

public interface IAuthService
{
    public Task<TypedResult<string>> LoginAsync(LoginRequestDTO request);
}

