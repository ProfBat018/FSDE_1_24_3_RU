using AuthApi.Application.Services.Interfaces;
using AuthApi.Application.Utils;
using AuthApi.Core.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly TokenManager _tokenManager;
    private readonly IUserService _userService;
    
    public AccountController(IAccountService accountService,  IUserService userService, TokenManager tokenManager)
    {
        _accountService = accountService;
        _userService = userService;
        _tokenManager = tokenManager;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequestDTO requestDto)
    {
        var res = await _accountService.RegisterAsync(requestDto);
        return Ok(res);
    }

    [Authorize]
    [HttpPost("Confirm")]
    public async Task<IActionResult> ConfirmEmailAsync()
    {
        var token = await _tokenManager.CreateEmailTokenAsync(User);
        await _accountService.ConfirmEmailAsync(User, token, HttpContext);

        return Ok("Email Confirmation Sent");
    }

    [HttpGet("Verify/{id}/{token}")]
    public async Task<IActionResult> VerifyEmailAsync(string id, string token)
    {
        var res = await _tokenManager.ValidateEmailTokenAsync(token);
        if (!res)
        {
            throw new Exception("Error token confirmation");   
        }
        var emailFromToken = await _tokenManager.GetEmailFromToken(token);
        var idByEmail = await _userService.GetIdByEmailAsync(emailFromToken);

        if (id == idByEmail)
        {
            return Ok(await _accountService.VerifyEmailAsync(id));
        }

        throw new Exception("Error token confirmation");
    }
}

