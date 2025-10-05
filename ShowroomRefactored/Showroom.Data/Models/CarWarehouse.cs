using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Data.Models;


public class CarWarehouse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CarId { get; set; }
    public string WarehouseId { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
    public Warehouse Warehouse { get; set; }
    public Car Car { get; set; }
}
