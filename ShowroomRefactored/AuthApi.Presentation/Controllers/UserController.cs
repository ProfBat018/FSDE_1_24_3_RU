using System.Globalization;
using AuthApi.Application.Services.Interfaces;
using AuthApi.Core.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Info")]
    public async Task<IActionResult> AddUserInfo([FromBody] UserInfoRequestDTO requestDto)
    {
        var res = await _userService.AddUserInfoAsync(User, requestDto);
        return Ok(res);
    }
    
    
    [HttpGet("Info")]
    public async Task<IActionResult> GetMyInfo()
    {
        var culture = Request.Headers["Accept-Language"].ToString();
        var culture2 = CultureInfo.CurrentCulture;
        var result = await _userService.GetUserInfoAsync(User);
        return Ok(result);
    }
}