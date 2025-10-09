using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Data.Models;


public class CarOrder
{
    public string OrderId { get; set; }
    public string CarId { get; set; }
    public int CarCount { get; set; }
}
