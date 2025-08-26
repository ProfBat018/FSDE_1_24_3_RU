using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Models;

public class Category
{
    public string CategoryName { get; set; }
    public string? ParentCategoryName { get; set; } = null;
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<CategoryAttributes> CategoryAttributes { get; set; }
}
