using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;


public class HubObject
{
    public string Id { get; private set; }
    public string HubName { get; private set; }

    public string ProductWarehouseId { get; private set; }
    public ProductWarehouse ProductWarehouse { get; private set; }

    protected HubObject() { }

    public HubObject(string hubName, ProductWarehouse warehouse)
    {
        Id = Guid.NewGuid().ToString();
        SetName(hubName);
        ProductWarehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));
        ProductWarehouseId = warehouse.Id;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Hub name cannot be empty.");
        HubName = name;
    }
}