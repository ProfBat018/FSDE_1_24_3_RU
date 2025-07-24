using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers;

[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    public async Task<IActionResult> Register()
    {
        throw new NotImplementedException();
    }
    
}