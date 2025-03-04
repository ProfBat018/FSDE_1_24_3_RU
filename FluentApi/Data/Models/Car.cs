using System.ComponentModel.DataAnnotations;


namespace FluentApi.Data.Models;

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; } = "Black";
    public DateTime ProductionDate { get; set; } = DateTime.Now;
   
    public int CarTypeId { get; set; }
    public CarType CarType { get; set; }

    public int FuelTypeId { get; set; }
    public FuelType FuelType { get; set; }

    public ICollection<Sale> Sales { get; set; }
}