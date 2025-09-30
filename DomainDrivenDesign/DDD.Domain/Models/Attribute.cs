using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DDD.Domain.Models;

public class Attribute
{
    public string Id { get; private set; }
    public string AttributeName { get; private set; }

    private readonly List<AttributeValue> _attributeValues = new();
    private readonly List<CategoryAttributes> _categoryAttributes = new();

    public IReadOnlyCollection<AttributeValue> AttributeValues => _attributeValues.AsReadOnly();
    public IReadOnlyCollection<CategoryAttributes> CategoryAttributes => _categoryAttributes.AsReadOnly();

    protected Attribute() { }

    public Attribute(string attributeName)
    {
        Id = Guid.NewGuid().ToString();
        SetName(attributeName);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Attribute name cannot be empty.");
        AttributeName = name;
    }

    public void AddValue(string value)
    {
        if (_attributeValues.Any(v => v.Value == value))
            throw new InvalidOperationException("Duplicate attribute value.");
        _attributeValues.Add(new AttributeValue(value, this));
    }
}
