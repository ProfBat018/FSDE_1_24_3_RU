using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;

public class Order
{
    public string Id { get; private set; }
    public string UserId { get; private set; }
    public string HubId { get; private set; }
    public HubObject Hub { get; private set; }

    private readonly List<OrderProduct> _products = new();
    public IReadOnlyCollection<OrderProduct> Products => _products.AsReadOnly();

    public double TotalPrice => _products.Sum(p => p.TotalPrice);

    protected Order() { }

    public Order(string userId, HubObject hub)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        Hub = hub ?? throw new ArgumentNullException(nameof(hub));
        HubId = hub.Id;
    }

    public void AddProduct(Product product, int quantity, double pricePerUnit)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        _products.Add(new OrderProduct(this, product, quantity, pricePerUnit));
    }
}