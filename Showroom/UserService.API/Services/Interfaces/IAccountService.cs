using UserService.API.DTO.Response;

namespace UserService.API.Services.Interfaces;

public interface IAccountService
{
    public Task<Result> RegisterAsync();

}