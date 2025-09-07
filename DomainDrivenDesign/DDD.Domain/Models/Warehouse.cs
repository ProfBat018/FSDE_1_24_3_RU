using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;


public class Warehouse
{
    public string Id { get; private set; }
    public string Address { get; private set; }

    private readonly List<HubObject> _hubs = new();
    public IReadOnlyCollection<HubObject> Hubs => _hubs.AsReadOnly();

    protected Warehouse() { }

    public Warehouse(string address)
    {
        Id = Guid.NewGuid().ToString();
        SetAddress(address);
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be empty.");
        Address = address;
    }

    public void AddHub(HubObject hub)
    {
        if (hub == null) throw new ArgumentNullException(nameof(hub));
        _hubs.Add(hub);
    }
}