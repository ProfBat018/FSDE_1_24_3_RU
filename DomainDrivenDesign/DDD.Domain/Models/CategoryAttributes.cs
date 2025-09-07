using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;



public class CategoryAttributes
{
    public string CategoryId { get; private set; }
    public string AttributeId { get; private set; }

    public Category Category { get; private set; }
    public Attribute Attribute { get; private set; }

    protected CategoryAttributes() { }

    public CategoryAttributes(Category category, Attribute attribute)
    {
        Category = category ?? throw new ArgumentNullException(nameof(category));
        Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));

        CategoryId = category.CategoryName;
        AttributeId = attribute.Id;
    }
}