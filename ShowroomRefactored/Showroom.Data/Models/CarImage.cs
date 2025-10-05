using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Data.Models;


public class CarImage
{

    public string ImageName { get; set; }
    public string CarId { get; set; }
    public bool IsMain { get; set; }
    public string ImagePath { get; set; }
    public Car Car { get; set; }
}
