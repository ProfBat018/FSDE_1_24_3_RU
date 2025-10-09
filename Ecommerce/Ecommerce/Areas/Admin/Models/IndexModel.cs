using Azure.Identity;
using ProductRepository.Models;

namespace Ecommerce.Areas.Admin.Models;

public class IndexModel
{
    public List<Product> Products { get; set; }
    public List<Category> Categories { get; set; }
}