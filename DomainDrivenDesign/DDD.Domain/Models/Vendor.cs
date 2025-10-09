using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;

public class Vendor
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string VendorName { get; set; }

    public ICollection<Product> Products { get; set; }
}
