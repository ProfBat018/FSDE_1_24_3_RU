using UserService.Contracts.Response;
using UserService.Contracts.DTOs;

namespace UserService.Application.Interfaces;

public interface IUserService
{
    Task<Result<IEnumerable<UserDto>>> GetAllAsync();
    Task<Result<UserDto>> GetByIdAsync(string id);
    Task<Result<UserDto>> CreateAsync(CreateUserDto dto);
    Task<Result<UserDto>> UpdateAsync(string id, CreateUserDto dto);
    Task<Result<string>> DeleteAsync(string id);
    Task<Result<UserDto>> GetByEmailAsync(string email);


}