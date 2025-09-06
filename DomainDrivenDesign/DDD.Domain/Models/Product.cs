using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductName { get; set; }
    public string Description { get; set; }
    public string VendorId { get; set; }
    public Vendor Vendor { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; }
    public ICollection<ProductCategory> ProductCategories  { get; set; }
}
