using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;


public class Product
{
    public string Id { get; private set; }
    public string ProductName { get; private set; }
    public string Description { get; private set; }

    public string VendorId { get; private set; }
    public Vendor Vendor { get; private set; }

    private readonly List<ProductImage> _images = new();
    private readonly List<ProductCategory> _categories = new();

    public IReadOnlyCollection<ProductImage> ProductImages => _images.AsReadOnly();
    public IReadOnlyCollection<ProductCategory> ProductCategories => _categories.AsReadOnly();

    protected Product() { }

    public Product(string name, string description, Vendor vendor)
    {
        Id = Guid.NewGuid().ToString();
        SetName(name);
        Description = description ?? "";
        Vendor = vendor ?? throw new ArgumentNullException(nameof(vendor));
        VendorId = vendor.Id;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.");
        ProductName = name;
    }

    public void AddImage(ProductImage image)
    {
        if (image == null) throw new ArgumentNullException(nameof(image));
        _images.Add(image);
    }

    public void AddCategory(ProductCategory category)
    {
        if (category == null) throw new ArgumentNullException(nameof(category));
        _categories.Add(category);
    }
}