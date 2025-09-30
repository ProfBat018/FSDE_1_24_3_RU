using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;



public class ProductWarehouse
{
    public string Id { get; private set; }

    public string ProductId { get; private set; }
    public Product Product { get; private set; }

    public string WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; }

    public int Count { get; private set; }
    public double Price { get; private set; }

    protected ProductWarehouse() { }

    public ProductWarehouse(Product product, Warehouse warehouse, int count, double price)
    {
        Id = Guid.NewGuid().ToString();
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Warehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));
        ProductId = product.Id;
        WarehouseId = warehouse.Id;
        SetStock(count, price);
    }

    public void SetStock(int count, double price)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
        Count = count;
        Price = price;
    }

    public void AddStock(int quantity)
    {
        if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Count += quantity;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity <= 0 || quantity > Count)
            throw new InvalidOperationException("Invalid quantity to reduce.");
        Count -= quantity;
    }
}