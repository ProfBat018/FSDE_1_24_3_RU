using UserService.API.DTOs.Requests;
using UserService.API.DTOs.Response;

namespace UserService.API.Services.Interfaces;

public interface IAccountService
{
    public Task<Result> RegisterAsync(RegisterRequestDTO requestDto);

}