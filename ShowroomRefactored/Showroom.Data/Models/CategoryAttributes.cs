using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Data.Models;


public class CategoryAttributes
{
    public Category  Category{ get; set; }
    public Models.Attribute Attribute { get; set; }
    public string CategoryId { get; set; }
    public string AttributeId { get; set; }
}
