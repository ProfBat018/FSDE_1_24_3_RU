using System.Security.Claims;
using AuthApi.Core.DTOs.Request;
using AuthApi.Core.DTOs.Response;

namespace AuthApi.Application.Services.Interfaces;

public interface IUserService
{
    public Task<string> GetIdByEmailAsync(string email);
    public Task<TypedResult<string>> GetUserInfoAsync(ClaimsPrincipal userClaims);
    public Task<Result> AddUserInfoAsync(ClaimsPrincipal userClaims, UserInfoRequestDTO request);

}