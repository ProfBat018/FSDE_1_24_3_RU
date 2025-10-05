using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;


public class OrderProduct
{
    public string OrderId { get; set; }
    public string ProductId { get; set; }
    public int ProductCount { get; set; }
}
