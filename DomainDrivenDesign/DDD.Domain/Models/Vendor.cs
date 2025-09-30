using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;


public class Vendor
{
    public string Id { get; private set; }
    public string VendorName { get; private set; }

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    protected Vendor() { }

    public Vendor(string vendorName)
    {
        Id = Guid.NewGuid().ToString();
        SetName(vendorName);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Vendor name cannot be empty.");
        VendorName = name;
    }

    public void AddProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        _products.Add(product);
    }
}