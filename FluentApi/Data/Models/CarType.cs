using System.ComponentModel.DataAnnotations;

namespace FluentApi.Data.Models;

public class CarType
{
    public int Id { get; set; }
    public string CarTypeName { get; set; }
    
    public ICollection<Car> Cars { get; set; }
}