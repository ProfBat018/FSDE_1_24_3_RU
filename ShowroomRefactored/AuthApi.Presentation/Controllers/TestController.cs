
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Presentation.Controllers;

[ApiController]
[Authorize]
public class TestController : ControllerBase
{
    [HttpGet("api/test/secure")]
    public IActionResult GetSecureData()
    {
        return Ok("This is a secure endpoint. You are authenticated!");
    }
    
}