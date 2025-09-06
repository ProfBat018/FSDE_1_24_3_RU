using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;


public class ProductImage
{
    // Unique and at the same time PK 
    public string ImageName { get; set; }
    public string ProductId { get; set; }
    public bool IsMain { get; set; }
    public string ImagePath { get; set; }
    public Product Product { get; set; }
}
