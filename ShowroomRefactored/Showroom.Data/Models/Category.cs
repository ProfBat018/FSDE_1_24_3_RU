using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Data.Models;


public class Category
{
    public string CategoryName { get; set; }
    public string? ParentCategoryName { get; set; } = null;
    public ICollection<CarCategory> CarCategories { get; set; }
    public ICollection<CategoryAttributes> CategoryAttributes { get; set; }
}
