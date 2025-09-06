using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;


public class Warehouse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Address { get; set; }

    public ICollection<HubObject> Hubs { get; set; }
}
