using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllerFirst.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase 
{
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("VerifyEmail")]
    public async Task<IActionResult> VerifyEmailConfirmationAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPasswordAsync()
    {
        throw new NotImplementedException();
    }
    
    
    
}