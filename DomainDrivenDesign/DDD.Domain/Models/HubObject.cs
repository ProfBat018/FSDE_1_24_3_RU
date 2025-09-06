using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Models;

public class HubObject
{
    public string Id { get; set; }
    public string HubName { get; set; }
    public string ProductWarehouseId { get; set; }
    public ProductWarehouse ProductWarehouse { get; set; }
}
