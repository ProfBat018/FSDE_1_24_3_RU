using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using UserService.Contracts.Response;
using UserService.Contracts.DTOs;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<Result<IEnumerable<UserDto>>> GetAll()
    {
        return await _userService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<Result<UserDto>> Get(string id)
    {
        return await _userService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<Result<UserDto>> Create([FromBody] CreateUserDto dto)
    {
        return await _userService.CreateAsync(dto);
    }

    [HttpPut("{id}")]
    public async Task<Result<UserDto>> Update(string id, [FromBody] CreateUserDto dto)
    {
        return await _userService.UpdateAsync(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task<Result<string>> Delete(string id)
    {
        return await _userService.DeleteAsync(id);
    }
}
