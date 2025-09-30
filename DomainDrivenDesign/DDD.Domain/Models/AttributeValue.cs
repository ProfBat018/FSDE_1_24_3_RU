using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;



public class AttributeValue
{
    public string Value { get; private set; }
    public string AttributeId { get; private set; }
    public Attribute Attribute { get; private set; }

    protected AttributeValue() { }

    public AttributeValue(string value, Attribute attribute)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or empty.");

        Value = value;
        Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        AttributeId = attribute.Id;
    }
}