using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;



public class ProductImage
{
    public string ImageName { get; private set; }  // PK
    public string ProductId { get; private set; }
    public string ImagePath { get; private set; }
    public bool IsMain { get; private set; }

    public Product Product { get; private set; }

    protected ProductImage() { }

    public ProductImage(string imageName, string path, bool isMain, Product product)
    {
        if (string.IsNullOrWhiteSpace(imageName))
            throw new ArgumentException("Image name cannot be empty.");
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Image path cannot be empty.");

        ImageName = imageName;
        ImagePath = path;
        IsMain = isMain;
        Product = product ?? throw new ArgumentNullException(nameof(product));
        ProductId = product.Id;
    }

    public void SetAsMain() => IsMain = true;
    public void UnsetAsMain() => IsMain = false;
}
