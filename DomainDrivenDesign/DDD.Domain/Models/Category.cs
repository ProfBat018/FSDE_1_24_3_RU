using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DDD.Domain.Models;



public class Category
{
    public string CategoryName { get; private set; }
    public string? ParentCategoryName { get; private set; }

    private readonly List<ProductCategory> _productCategories = new();
    private readonly List<CategoryAttributes> _categoryAttributes = new();

    public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories.AsReadOnly();
    public IReadOnlyCollection<CategoryAttributes> CategoryAttributes => _categoryAttributes.AsReadOnly();

    protected Category() { }

    public Category(string name, string? parent = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.");
        CategoryName = name;
        ParentCategoryName = parent;
    }

    public void SetParent(string? parentName)
    {
        ParentCategoryName = parentName;
    }
}