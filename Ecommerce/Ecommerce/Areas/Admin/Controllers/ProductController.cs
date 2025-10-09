using Ecommerce.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/admin/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("All/{page}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int page=1,  int pageSize=15)
    {
        var res = await _productService.GetAllProductsAsync(page, pageSize);
        return Ok(res);
    }
}