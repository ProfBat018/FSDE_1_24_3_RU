using AuthApi.Application.Services.Interfaces;
using AuthApi.Core.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Presentation.Controllers;


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
        var res = await _authService.LoginAsync(request);
        
        return Ok(res);
    }


    [Authorize(Policy = "UserPolicy")]
    [HttpGet("Test")]
    public async Task<IActionResult> TestAsync()
    {
        return Ok("success");
    }
}