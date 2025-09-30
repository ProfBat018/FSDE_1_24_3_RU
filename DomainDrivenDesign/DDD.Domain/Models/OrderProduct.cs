using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;



public class OrderProduct
{
    public string OrderId { get; private set; }
    public Order Order { get; private set; }

    public string ProductId { get; private set; }
    public Product Product { get; private set; }

    public int ProductCount { get; private set; }
    public double UnitPrice { get; private set; }

    public double TotalPrice => ProductCount * UnitPrice;

    protected OrderProduct() { }

    public OrderProduct(Order order, Product product, int count, double unitPrice)
    {
        Order = order ?? throw new ArgumentNullException(nameof(order));
        Product = product ?? throw new ArgumentNullException(nameof(product));

        OrderId = order.Id;
        ProductId = product.Id;

        if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice));

        ProductCount = count;
        UnitPrice = unitPrice;
    }
}