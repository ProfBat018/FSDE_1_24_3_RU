using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Models;

public class ProductWarehouse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductId { get; set; }
    public string WarehouseId { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
    public Warehouse Warehouse { get; set; }
    public Product Product { get; set; }
}
