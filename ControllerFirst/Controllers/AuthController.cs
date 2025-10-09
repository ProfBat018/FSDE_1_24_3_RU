using Microsoft.AspNetCore.Mvc;

namespace ControllerFirst.Controllers;


// localhost:5001/api/Auth/Login

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync()
    {
        return Ok(new
        {
            Message = "User Created"
        });
    }
}
