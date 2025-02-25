using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Models;

public class Car
{
    [Key]
    public int Id { get; set; }
   
    [Required]
    [MaxLength(50)]
    public string Make { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Model { get; set; }

    [MaxLength(50)] 
    public string Color { get; set; } = "Black";
    
    public DateTime ProductionDate { get; set; } = DateTime.Now;

    [ForeignKey("CarType")]
    public int CarTypeId { get; set; }
    public CarType CarType { get; set; }
    
    public int FuelTypeId { get; set; }
    public FuelType FuelType { get; set; }
}
