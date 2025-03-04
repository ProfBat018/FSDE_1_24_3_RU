using System.ComponentModel.DataAnnotations;

namespace FluentApi.Data.Models;

public class FuelType
{
    public int Id { get; set; }   
    public string FuelTypeName { get; set; }   
    
    public ICollection<Car> Cars { get; set; }
}