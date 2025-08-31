using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/admin/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet("Product/All")]
    public async Task<IActionResult> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}