using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Data.Models;

public class FuelType
{
    [Key]
    public int Id { get; set; }   
    
    [Required]
    [MaxLength(50)]
    public string FuelTypeName { get; set; }   
}