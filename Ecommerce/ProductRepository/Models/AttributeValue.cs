using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Models;

public class AttributeValue
{
    public string Value { get; set; } // UNIQUE
    public string AttributeId { get; set; }
    public Attribute Attribute{ get; set; }
}
