using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Models;

public class Attribute
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    // Не должен быть уникальным !! 
    public string AttributeName { get; set; }

    public ICollection<Category> Categories { get; set; }
    public ICollection<AttributeValue> AttributeValues { get; set; }
    public ICollection<CategoryAttributes> CategoryAttributes { get; set; }
}
