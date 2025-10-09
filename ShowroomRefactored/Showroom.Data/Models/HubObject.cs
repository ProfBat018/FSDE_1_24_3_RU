using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Data.Models;


public class HubObject
{
    public string Id { get; set; }
    public string HubName { get; set; }
    public string CarWarehouseId { get; set; }
    public CarWarehouse CarWarehouse { get; set; }
}
