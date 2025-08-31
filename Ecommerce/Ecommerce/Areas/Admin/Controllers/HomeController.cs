using Ecommerce.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRepository.Contexts;

namespace Ecommerce.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    private readonly ProductsContext  _context;
    
    public HomeController(ProductsContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        IndexModel model = new();
        
        model.Categories = await  _context.Categories.ToListAsync();
        model.Products = await  _context.Products.ToListAsync();
        
        return View(model);
    }
}