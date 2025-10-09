using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Data.Models;

public class Car
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Model { get; set; }
    public string Description { get; set; }
    
    public string VendorId { get; set; }
    public Vendor Vendor { get; set; }

    public ICollection<CarImage> ProductImages { get; set; }
    public ICollection<CarCategory> ProductCategories  { get; set; }
}
