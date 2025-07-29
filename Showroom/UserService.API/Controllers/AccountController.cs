using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.DTOs.Requests;
using UserService.API.Services.Interfaces;

namespace UserService.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
private readonly ITokenService _tokenService;
private readonly IUserService _userService;
    
    public AccountController(IAccountService accountService, ITokenService tokenService, IUserService userService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
        _userService = userService;
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
        var token = await _tokenService.CreateEmailTokenAsync(User);
        await _accountService.ConfirmEmailAsync(User, token, HttpContext);

        return Ok("Email Confirmation Sent");
    }

    [HttpGet("Verify/{id}/{token}")]
    public async Task<IActionResult> VerifyEmailAsync(string id, string token)
    {
        var res = await _tokenService.ValidateEmailTokenAsync(token);
        if (!res)
        {
            throw new Exception("Error token confirmation");   
        }
        var emailFromToken = await _tokenService.GetEmailFromToken(token);
        var idByEmail = await _userService.GetIdByEmailAsync(emailFromToken);

        if (id == idByEmail)
        {
            return Ok(await _accountService.VerifyEmailAsync(id));
        }

        throw new Exception("Error token confirmation");
    }
}

