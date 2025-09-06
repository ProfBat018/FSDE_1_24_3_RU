using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;


public class ProductCategory
{
    public string ProductCategoryId { get; set; } = Guid.NewGuid().ToString();
    public string ProductId { get; set; }
    public string CategoryId { get; set; }

    public Product Product { get; set; }
    public Category Category { get; set; }


}
