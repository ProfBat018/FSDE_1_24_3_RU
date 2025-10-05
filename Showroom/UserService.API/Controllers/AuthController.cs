using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.DTOs.Requests;
using UserService.API.Services.Interfaces;
using UserService.Data.Data.Models;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(LoginRequestDTO request)
    {
        return Ok(await _authService.LoginAsync(request));
    }

    // [Authorize]
    // [HttpPost("Refresh")]
    // public async Task<IActionResult> RefreshTokenAsync()
    // {
    //     
    // }

    [Authorize]
    [HttpPost("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        
        return Ok("Logout successful");
    }
    
    [Authorize(Policy = "UserPolicy")]
    [HttpGet("Test")]
    public async Task<IActionResult> TestAsync()
    {
        return Ok("success");
    }
}