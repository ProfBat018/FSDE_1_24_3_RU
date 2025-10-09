using Ecommerce.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRepository.Contexts;

namespace Ecommerce.Areas.Admin.Controllers;

[Authorize(Policy = "AdminPolicy")]
[Area("Admin")]
public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}