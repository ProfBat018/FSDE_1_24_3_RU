using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Data.Models;


public class CarCategory
{
    public string CarCategoryId { get; set; } = Guid.NewGuid().ToString();
    
    public string CarId { get; set; }
    public string CategoryId { get; set; }

    public Car Car { get; set; }
    public Category Category { get; set; }


}
