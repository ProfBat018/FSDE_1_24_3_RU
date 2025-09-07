using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;



public class ProductCategory
{
    public string ProductCategoryId { get; private set; }
    public string ProductId { get; private set; }
    public string CategoryId { get; private set; }

    public Product Product { get; private set; }
    public Category Category { get; private set; }

    protected ProductCategory() { }

    public ProductCategory(Product product, Category category)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Category = category ?? throw new ArgumentNullException(nameof(category));

        ProductId = product.Id;
        CategoryId = category.CategoryName;
        ProductCategoryId = Guid.NewGuid().ToString();
    }
}